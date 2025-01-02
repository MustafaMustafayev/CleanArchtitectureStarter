using Application.Pagination;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.ErrorLogs.Queries;
public sealed class GetErrorLogPaginatedListQueryHandler(IMapper mapper,
                                                         IErrorLogRepository errorLogRepository) : IRequestHandler<GetErrorLogPaginatedListQuery, IDataResult<PaginatedResult<GetErrorLogPaginatedListResponse>>>
{
    public async Task<IDataResult<PaginatedResult<GetErrorLogPaginatedListResponse>>> Handle(GetErrorLogPaginatedListQuery request, CancellationToken cancellationToken)
    {
        (IEnumerable<ErrorLog> datas, int totalCount) = await errorLogRepository.GetPaginatedListAsNoTrackingAsync(request.PageNumber, request.PageSize);

        PaginatedResult<GetErrorLogPaginatedListResponse> result;

        if (!datas.Any())
        {
            result = new PaginatedResult<GetErrorLogPaginatedListResponse>(Enumerable.Empty<GetErrorLogPaginatedListResponse>().ToList(),
                                                                       totalCount,
                                                                       request.PageNumber,
                                                                       request.PageSize);
        }
        else
        {
            result = new PaginatedResult<GetErrorLogPaginatedListResponse>(mapper.Map<List<GetErrorLogPaginatedListResponse>>(datas),
                                                                       totalCount,
                                                                       request.PageNumber,
                                                                       request.PageSize);
        }

        return new SuccessDataResult<PaginatedResult<GetErrorLogPaginatedListResponse>>(result);
    }
}