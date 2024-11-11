using FluentValidation;

namespace Application.Features.Auth.Login;
public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        
    }
}
