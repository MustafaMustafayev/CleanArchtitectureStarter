namespace Application.Dtos.ErrorLogs;
public sealed record ErrorLogCreateDto
{
    public string? AccessToken { get; set; }
    public Guid? UserId { get; set; }
    public string? Path { get; set; }
    public string? Ip { get; set; }
    public string? ErrorMessage { get; set; }
    public string? StackTrace { get; set; }
    public string? TraceIdentifier { get; set; }
}