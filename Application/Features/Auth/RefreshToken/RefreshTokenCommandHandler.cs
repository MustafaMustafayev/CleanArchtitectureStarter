using Application.Responses;
using MediatR;

namespace Application.Features.Auth.RefreshToken;
public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, IDataResult<RefreshTokenResponse>>
{
    public async Task<IDataResult<RefreshTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}
