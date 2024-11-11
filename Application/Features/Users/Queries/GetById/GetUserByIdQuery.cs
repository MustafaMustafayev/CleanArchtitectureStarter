using Application.Responses;
using MediatR;

namespace Application.Features.Users.Queries.GetById;
public sealed record GetUserByIdQuery : IRequest<IDataResult<GetUserByIdResponse>>
{
    public Guid Id { get; set; }
}
