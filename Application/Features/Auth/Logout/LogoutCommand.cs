using Application.Responses;
using MediatR;

namespace Application.Features.Auth.Logout;
public sealed record LogoutCommand : IRequest<IResult>
{
}