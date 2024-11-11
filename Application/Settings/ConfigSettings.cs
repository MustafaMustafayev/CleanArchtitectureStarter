namespace Application.Settings;
public sealed record ConfigSettings
{
    public AuthSettings AuthSettings { get; set; } = default!;
    public DatabaseSettings DatabaseSettings { get; set; } = default!;
    public CryptographySettings CryptographySettings { get; set; } = default!;
    public SwaggerSettings SwaggerSettings { get; set; } = default!;
}
