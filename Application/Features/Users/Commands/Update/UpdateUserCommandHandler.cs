using Application.Localization;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Users.Commands.Update;
public sealed class UpdateUserCommandHandler(IMapper mapper,
                                             IUserRepository userRepository,
                                             IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserCommand, IResult>
{
    public async Task<IResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if(await userRepository.IsEmailExistAsync(request.Id, request.Email))
        {
            return new ErrorResult(EMessages.EmailAlreadyExist.Translate());
        }

        User user = mapper.Map<User>(request);
        await userRepository.UpdateAsync(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new SuccessResult();
    }
}
