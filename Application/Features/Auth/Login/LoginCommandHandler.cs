using Application.Dtos.Tokens;
using Application.Dtos.Users;
using Application.Helpers;
using Application.Interfaces;
using Application.Localization;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Auth.Login;
public sealed class LoginCommandHandler(IMapper mapper,
                                        IUserRepository userRepository,
                                        ITokenService tokenService,
                                        IUnitOfWork unitOfWork) : IRequestHandler<LoginCommand, IDataResult<LoginCommandResponse>>
{
    public async Task<IDataResult<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetAsNoTrackingAsync(m => m.Email == request.Email);
        if (user is not { })
        {
            return new ErrorDataResult<LoginCommandResponse>(EMessages.InvalidLoginCredentials.Translate());
        }

        string passwordHash = SecurityHelper.HashPassword(request.Password, user.PasswordSalt);

        if (user.PasswordHash != passwordHash)
        {
            return new ErrorDataResult<LoginCommandResponse>(EMessages.InvalidLoginCredentials.Translate());
        }

        TokenInfoDto tokenInfo = await tokenService.GenerateTokenAsync(user.Id, user.Email);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        LoginCommandResponse loginCommandResponse = new()
        {
            AccessToken = tokenInfo.AccessToken,
            AccessTokenExpireDate = tokenInfo.AccessTokenExpireDate,
            User = mapper.Map<UserLoginInfoDto>(user)
        };

        return new SuccessDataResult<LoginCommandResponse>(loginCommandResponse);
    }
}