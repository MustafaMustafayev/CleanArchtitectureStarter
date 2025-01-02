namespace Application.Features.Users.Queries.GetPaginatedList;
public sealed record GetUserPaginatedListResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Surname { get; set; } = default!;
    public string Email { get; set; } = default!;
}