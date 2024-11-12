using Application.Features.ErrorLogs.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ErrorLogController(IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("{pageNumber:int}/{pageSize:int}")]
    public async Task<IActionResult> Get([FromRoute] int pageNumber, int pageSize)
    {
        GetErrorLogPaginatedListQuery request = new()
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var response = await mediator.Send(request);
        return Ok(response);
    }
}
