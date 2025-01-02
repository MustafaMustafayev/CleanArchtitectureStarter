namespace Application.Settings;
public sealed record DatabaseSettings
{
    public required string ConnectionString { get; set; }
    public required byte MaxRetryCount { get; set; }
    public required byte CommandTimeout { get; set; }
    public required bool EnableDetailedErrors { get; set; }
}