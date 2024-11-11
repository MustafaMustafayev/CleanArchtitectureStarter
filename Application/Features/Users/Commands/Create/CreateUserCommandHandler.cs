using Application.Helpers;
using Application.Localization;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Users.Commands.Create;
public sealed class CreateUserCommandHandler(IMapper mapper, 
                                             IUserRepository  userRepository,
                                             IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, IResult>
{
    public async Task<IResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if(await userRepository.IsEmailExistAsync(null, request.Email))
        {
            return new ErrorResult(EMessages.EmailAlreadyExist.Translate());
        }

        User user = mapper.Map<User>(request);
        user.PasswordSalt = SecurityHelper.GenerateSalt();
        user.PasswordHash = SecurityHelper.HashPassword(request.Password, user.PasswordSalt);

        await userRepository.AddAsync(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new SuccessResult();
    }
}
