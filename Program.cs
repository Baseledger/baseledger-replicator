using System.Reflection;
using baseledger_replicator.BusinessLogic.Transactions.TransactionAgent;
using baseledger_replicator.Common.ExceptionHandling;
using baseledger_replicator.Models;
using baseledger_replicator.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
        .Enrich.FromLogContext()
        .WriteTo.File("Logs/baseledger-replicator.txt", rollingInterval: RollingInterval.Day)
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));

// Add services to the container.
string connectionString = builder.Configuration["ConnectionStrings:Postgres"] + builder.Configuration["ConnectionStrings:PostgresPassword"];
builder.Services.AddDbContextPool<BaseledgerReplicatorContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<BaseledgerReplicatorContext>();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register interfaces
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ITransactionAgent, TransactionAgent>();

builder.Services.AddControllers();

builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            RequireExpirationTime = true,
            RequireSignedTokens = true,
            ClockSkew = TimeSpan.FromMinutes(5),

            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(builder.Configuration["JWT:Secret"]))
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "baseledger-replicator-api", Version = "v1" });

    var security = new Dictionary<string, IEnumerable<string>>
    {
        {"Bearer", Array.Empty<string>()},
    };

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { new OpenApiSecurityScheme { Reference = new OpenApiReference
        {
            Id = "Bearer",
            Type = ReferenceType.SecurityScheme
        }}, new List<string>() }
    });
});

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseCustomExceptionHandler();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<BaseledgerReplicatorContext>();
    dataContext.Database.Migrate();

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    var existingAdminUser = await userManager.FindByEmailAsync(builder.Configuration["AdminEmail"]);
    if (existingAdminUser == null)
    {
        var newUser = new IdentityUser()
        {
            Email = builder.Configuration["AdminEmail"],
            UserName = builder.Configuration["AdminEmail"],
            EmailConfirmed = true
        };
    
        await userManager.CreateAsync(newUser, builder.Configuration["AdminPassword"]);
    }
}

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "baseledger-replicator-api v1");
            c.RoutePrefix = string.Empty;
        });
// }

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
