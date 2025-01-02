using Application.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Users.Commands.Update;
public sealed record UpdateUserCommand : IRequest<IResult>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Surname { get; set; } = default!;
    [EmailAddress]
    public string Email { get; set; } = default!;
}