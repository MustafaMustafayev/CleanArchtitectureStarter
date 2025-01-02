using Application.Pagination;
using Application.Responses;
using MediatR;

namespace Application.Features.Users.Queries.GetPaginatedList;
public sealed record GetUserPaginatedListQuery : IRequest<IDataResult<PaginatedResult<GetUserPaginatedListResponse>>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}