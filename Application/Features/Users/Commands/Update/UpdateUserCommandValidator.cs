using FluentValidation;

namespace Application.Features.Users.Commands.Update;
public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(m => m.Name).NotNull().NotEmpty();
        RuleFor(m => m.Surname).NotNull().NotEmpty();
        RuleFor(m => m.Email).EmailAddress();
    }
}