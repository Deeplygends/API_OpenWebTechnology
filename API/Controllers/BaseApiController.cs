using System;
using Application.Wrapper;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.Extensions.DependencyInjection;


namespace API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected IActionResult HttpResponseResult<T>(Response<T> response)
        {
            switch (response.HttpResponse)
            {
                case HttpResponseTypeEnum.Ok:
                    return Ok(response);
                case HttpResponseTypeEnum.BadRequest:
                    return BadRequest(response);
                case HttpResponseTypeEnum.Created:
                    return Ok(response);
                case HttpResponseTypeEnum.Conflict:
                    return Conflict(response);
                case HttpResponseTypeEnum.Unauthorized:
                    return Unauthorized(response);
                case HttpResponseTypeEnum.NoContent:
                    return NoContent();
                default:
                    return BadRequest(response);
            }
        }
    }
}
