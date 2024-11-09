namespace Application.Settings;
public sealed class ConfigSettings
{
    public AuthSettings AuthSettings { get; set; } = default!;
    public CryptographySettings CryptographySettings { get; set; } = default!;
}
