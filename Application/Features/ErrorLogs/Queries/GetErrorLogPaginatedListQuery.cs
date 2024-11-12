using Application.Pagination;
using Application.Responses;
using MediatR;

namespace Application.Features.ErrorLogs.Queries;
public sealed record GetErrorLogPaginatedListQuery : IRequest<IDataResult<PaginatedResult<GetErrorLogPaginatedListResponse>>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
