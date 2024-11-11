using Application.Responses;
using MediatR;

namespace Application.Features.Auth.Login;
public sealed record LoginCommand : IRequest<IDataResult<LoginCommandResponse>>
{
}