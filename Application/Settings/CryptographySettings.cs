namespace Application.Settings;
public sealed record CryptographySettings
{
    public required string KeyBase64 { get; set; }
    public required string VBase64 { get; set; }
}
