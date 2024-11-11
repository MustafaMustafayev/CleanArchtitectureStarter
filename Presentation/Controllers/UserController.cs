using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Queries.GetList;
using Application.Features.Users.Queries.GetPaginatedList;
using Application.Pagination;
using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using IResult = Application.Responses.IResult;

namespace Presentation.Controllers;
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserController(IMediator mediator) : ControllerBase
{
    [SwaggerOperation(Summary = "get user list")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IDataResult<List<GetUserListResponse>>))]
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get()
    {
        GetUserListQuery request = new();
        var response = await mediator.Send(request);

        return Ok(response);
    }

    [SwaggerOperation(Summary = "get user paginated list")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IDataResult<PaginatedResult<GetUserPaginatedListResponse>>))]
    [HttpGet("{pageNumber:int}/{pageSize:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromRoute] int pageNumber, int pageSize, int a)
    {
        GetUserPaginatedListQuery request = new()
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var response = await mediator.Send(request);

        return Ok(response);
    }

    [SwaggerOperation(Summary = "get user by id")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IDataResult<GetUserByIdResponse>))]
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        GetUserByIdQuery request = new()
        {
            Id = id
        };

        var response = await mediator.Send(request);

        return Ok(response);
    }

    [SwaggerOperation(Summary = "create user")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IResult))]
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand request)
    {
        var response = await mediator.Send(request);
        return Ok(response);
    }

    [SwaggerOperation(Summary = "update user")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IResult))]
    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand request)
    {
        var response = await mediator.Send(request);
        return Ok(response);
    }

    [SwaggerOperation(Summary = "delete user")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IResult))]
    [HttpDelete("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeleteUserCommand command = new()
        {
            Id = id
        };

        var response = await mediator.Send(command);

        return Ok(response);
    }
}