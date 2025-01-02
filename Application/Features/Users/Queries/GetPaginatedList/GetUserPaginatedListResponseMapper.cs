using AutoMapper;
using Domain.Entities;

namespace Application.Features.Users.Queries.GetPaginatedList;
public sealed class GetUserPaginatedListResponseMapper : Profile
{
    public GetUserPaginatedListResponseMapper()
    {
        CreateMap<User, GetUserPaginatedListResponse>();
    }
}