using Application.Features.ErrorLogs.Queries;
using Application.Pagination;
using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ValidateToken]
public class ErrorLogsController(IMediator mediator) : ControllerBase
{
    [SwaggerOperation(Summary = "get errorlog paginated list")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IDataResult<PaginatedResult<GetErrorLogPaginatedListResponse>>))]
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