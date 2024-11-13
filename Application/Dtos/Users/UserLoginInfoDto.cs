using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Users;
public sealed record UserLoginInfoDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Surname { get; set; } = default!;
    public string Email { get; set; } = default!;
}
