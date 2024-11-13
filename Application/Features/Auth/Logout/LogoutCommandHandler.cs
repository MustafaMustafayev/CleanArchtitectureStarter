using Application.Interfaces;
using Application.Responses;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Auth.Logout;
public sealed class LogoutCommandHandler(ITokenRepository tokenRepository,
                                         ITokenResolverService tokenResolverService,
                                         IUnitOfWork unitOfWork) : IRequestHandler<LogoutCommand, IResult>
{
    public async Task<IResult> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        string accessToken = tokenResolverService.GetAccessToken();

        await tokenRepository.SoftDeleteAsync(accessToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new SuccessResult();
    }
}
