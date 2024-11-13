using Application.Dtos.Users;

namespace Application.Features.Auth.Login;
public sealed record LoginCommandResponse
{
    public UserLoginInfoDto User { get; set; } = default!;
    public string AccessToken { get; set; } = default!;
    public DateTime AccessTokenExpireDate { get; set; } = default!;
}