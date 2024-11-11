using Application.Responses;
using MediatR;

namespace Application.Features.Auth.RefreshToken;
public sealed record RefreshTokenCommand : IRequest<IDataResult<RefreshTokenResponse>>
{
}
