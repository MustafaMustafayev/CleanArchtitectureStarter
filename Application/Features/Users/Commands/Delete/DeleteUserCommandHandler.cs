using Application.Localization;
using Application.Responses;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Users.Commands.Delete;
public sealed class DeleteUserCommandHandler(IUserRepository userRepository,
                                             IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserCommand, IResult>
{
    public async Task<IResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        User user = await userRepository.GetAsync(request.Id);

        if(user is not { })
        {
            return new ErrorResult(EMessages.UserDoesNotExist.Translate());
        }

        userRepository.SoftDelete(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new SuccessResult();
    }
}
