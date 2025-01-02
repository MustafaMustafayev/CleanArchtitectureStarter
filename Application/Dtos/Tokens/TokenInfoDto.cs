namespace Application.Dtos.Tokens;
public sealed record TokenInfoDto
{
    public string AccessToken { get; set; } = default!;
    public DateTime AccessTokenExpireDate { get; set; } = default!;
}