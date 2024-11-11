namespace Application.Settings;
public sealed record SwaggerSettings : Controllable
{
    public required string Title { get; set; }
    public required string Version { get; set; }
    public required string Theme { get; set; }
}
