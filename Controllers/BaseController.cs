using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace baseledger_replicator.Controllers;

public class BaseController : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
#pragma warning disable CA1051 // Do not declare visible instance fields
    protected IMediator mediator;
#pragma warning restore CA1051 // Do not declare visible instance fields
    /// <summary>
    /// 
    /// </summary>
#pragma warning disable CA1051 // Do not declare visible instance fields
    protected IMapper mapper;
#pragma warning restore CA1051 // Do not declare visible instance fields

    /// <summary>
    /// 
    /// </summary>
    protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}