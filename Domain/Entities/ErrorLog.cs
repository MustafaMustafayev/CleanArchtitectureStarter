using Domain.Primitives;

namespace Domain.Entities;
public sealed class ErrorLog : IEntity
{
    public Guid Id { get; set; }
    public User? User { get; set; }
    public Guid? UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string AccessToken { get; set; } = string.Empty;
    public string Path { get; set; } = default!;
    public string Ip { get; set; } = default!;
    public string ErrorMessage { get; set; } = default!;
    public string StackTrace { get; set; } = default!;
    public string TraceIdentifier {  get; set; } = default!;
    public bool IsDeleted { get; set; }
}
