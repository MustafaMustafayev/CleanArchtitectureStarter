namespace Application.Features.Users.Queries.GetList;
public sealed record GetUserListResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Surname { get; set; } = default!;
    public string Email { get; set; } = default!;
}
