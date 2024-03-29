﻿using baseledger_replicator.Common.Exceptions;
using baseledger_replicator.Services;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Text;

namespace baseledger_replicator.Common.ExceptionHandling;
public static class ExceptionHandler
{
    /// <summary>
    /// Tracks custom exceptions that control the execution flow and formats the responses accordingly
    /// </summary>
    /// <param name="app"></param>
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(eApp =>
        {
            eApp.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorCtx = context.Features.Get<IExceptionHandlerFeature>();
                if (errorCtx != null)
                {
                    var ex = errorCtx.Error;
                    var message = "Unspecified error ocurred.";
                    var errors = new Dictionary<string, List<string>>();

                    switch (ex)
                    {
                        case ReplicatorNotFoundException:
                            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                            message = ex.Message;
                            break;
                        case ReplicatorValidationException:
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            message = ex.Message;
                            errors = (ex as dynamic).Errors;
                            break;
                        case ReplicatorUnauthorizedException:
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            message = ex.Message;
                            break;
                        case ReplicatorNodeException:
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            message = ex.Message;
                            break;
                        default:
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            message = ex.Message;
                            break;
                    }

                    var serializerSettings = new JsonSerializerSettings();
                    serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    var jsonResponse = JsonConvert.SerializeObject(new ErrorResponse
                    {
                        Message = message,
                        Errors = errors
                    }, serializerSettings);

                    await context.Response.WriteAsync(jsonResponse, Encoding.UTF8);
                }
            });
        });
    }
}
