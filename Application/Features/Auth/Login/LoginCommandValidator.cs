using FluentValidation;

namespace Application.Features.Auth.Login;
public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(m => m.Email).EmailAddress();
        RuleFor(m => m.Password).MinimumLength(7);
    }
}
