using Application.Responses;
using MediatR;

namespace Application.Features.Users.Commands.Delete;
public sealed record class DeleteUserCommand : IRequest<IResult>
{
    public Guid Id { get; set; }
}