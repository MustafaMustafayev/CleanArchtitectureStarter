using AutoMapper;
using Domain.Entities;

namespace Application.Features.Users.Queries.GetList;
public sealed class GetUserListResponseMapper : Profile
{
    public GetUserListResponseMapper()
    {
        CreateMap<User, GetUserListResponse>();
    }
}