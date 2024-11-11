using Application.Responses;
using MediatR;

namespace Application.Features.Users.Queries.GetList;
public sealed record GetUserListQuery : IRequest<IDataResult<List<GetUserListResponse>>>
{
}