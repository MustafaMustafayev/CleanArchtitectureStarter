using Application.Features.Auth.Login;
using Application.Features.Auth.Logout;
using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using IResult = Application.Responses.IResult;

namespace Presentation.Controllers;
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AuthController(IMediator mediator) : ControllerBase
{
    [SwaggerOperation(Summary = "login")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IDataResult<LoginCommandResponse>))]
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(response);
    }

    [SwaggerOperation(Summary = "logout")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IResult))]
    [ValidateToken]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        LogoutCommand command = new();

        var response = await mediator.Send(command);
        return Ok(response);
    }
}