using Application.Responses;
using MediatR;

namespace Application.Features.Auth.Login;
public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, IDataResult<LoginCommandResponse>>
{
    public async Task<IDataResult<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
