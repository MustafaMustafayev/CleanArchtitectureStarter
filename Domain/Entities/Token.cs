using Domain.Primitives;

namespace Domain.Entities;
public sealed class Token : IEntity
{
    public Guid Id { get; set; }
    public User User { get; set; } = default!;
    public Guid UserId { get; set; }
    public string AccessToken { get; set; } = default!;
    public DateTime AccessTokenExpireDate { get; set; }
    public string RefreshToken { get; set; } = default!;
    public DateTime RefreshTokenExpireDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
}
