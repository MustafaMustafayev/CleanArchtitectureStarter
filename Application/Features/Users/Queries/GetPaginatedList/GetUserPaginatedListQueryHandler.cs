﻿using Application.Pagination;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Users.Queries.GetPaginatedList;
public sealed class GetUserPaginatedListQueryHandler(IMapper mapper,
                                                     IUserRepository userRepository) : IRequestHandler<GetUserPaginatedListQuery, IDataResult<PaginatedResult<GetUserPaginatedListResponse>>>
{
    public async Task<IDataResult<PaginatedResult<GetUserPaginatedListResponse>>> Handle(GetUserPaginatedListQuery request, CancellationToken cancellationToken)
    {
        (IEnumerable<User> datas, int totalCount) paginatedResponse = await userRepository.GetPaginatedListAsNoTrackingAsync(request.PageNumber, request.PageSize);

        PaginatedResult<GetUserPaginatedListResponse> result
            ;
        if (!paginatedResponse.datas.Any())
        {
            result = new PaginatedResult<GetUserPaginatedListResponse>(Enumerable.Empty<GetUserPaginatedListResponse>().ToList(),
                                                                       paginatedResponse.totalCount,
                                                                       request.PageNumber,
                                                                       request.PageSize);
        }
        else
        {
            result = new PaginatedResult<GetUserPaginatedListResponse>(mapper.Map<List<GetUserPaginatedListResponse>>(paginatedResponse.datas),
                                                                       paginatedResponse.totalCount,
                                                                       request.PageNumber,
                                                                       request.PageSize);
        }

        return new SuccessDataResult<PaginatedResult<GetUserPaginatedListResponse>>(result);
    }
}