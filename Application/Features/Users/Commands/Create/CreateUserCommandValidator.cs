using FluentValidation;

namespace Application.Features.Users.Commands.Create;
public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(m => m.Name).NotNull().NotEmpty();
        RuleFor(m => m.Surname).NotNull().NotEmpty();
        RuleFor(m => m.Email).EmailAddress();
        RuleFor(m => m.Password).MinimumLength(7);
    }
}
