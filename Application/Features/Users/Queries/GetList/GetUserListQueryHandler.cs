using Application.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Users.Queries.GetList;
public sealed class GetUserListQueryHandler(IMapper mapper,
                                            IUserRepository userRepository) : IRequestHandler<GetUserListQuery, IDataResult<List<GetUserListResponse>>>
{
    public async Task<IDataResult<List<GetUserListResponse>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<User> users = await userRepository.GetListAsNoTrackingAsync();

        List<GetUserListResponse> dtos = mapper.Map<List<GetUserListResponse>>(users);

        return new SuccessDataResult<List<GetUserListResponse>>(dtos);
    }
}
