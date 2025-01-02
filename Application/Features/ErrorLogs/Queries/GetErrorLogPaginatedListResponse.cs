namespace Application.Features.ErrorLogs.Queries;
public sealed record GetErrorLogPaginatedListResponse
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public string CreatedAt { get; set; } = default!;
    public string AccessToken { get; set; } = string.Empty;
    public string Path { get; set; } = default!;
    public string Ip { get; set; } = default!;
    public string ErrorMessage { get; set; } = default!;
    public string StackTrace { get; set; } = default!;
    public string TraceIdentifier { get; set; } = default!;
}