namespace Application.Features.Users.Queries.GetById;
public sealed record GetUserByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Surname { get; set; } = default!;
    public string Email { get; set; } = default!;
}