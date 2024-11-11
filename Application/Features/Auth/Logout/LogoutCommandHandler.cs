using Application.Responses;
using MediatR;

namespace Application.Features.Auth.Logout;
public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand, IResult>
{
    public async Task<IResult> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
