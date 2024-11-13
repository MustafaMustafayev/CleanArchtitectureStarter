namespace Application.Settings;
public sealed record AuthSettings
{
    public required string Type { get; set; }
    public required string HeaderName { get; set; }
    public required string TokenPrefix { get; set; }
    public required string ContentType { get; set; }
    public required string SecretKey { get; set; }
    public required string TokenUserIdKey { get; set; }
    public required int TokenExpirationTimeInHours { get; set; }
}