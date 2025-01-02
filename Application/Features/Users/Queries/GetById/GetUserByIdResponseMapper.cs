using AutoMapper;
using Domain.Entities;

namespace Application.Features.Users.Queries.GetById;
public sealed class GetUserByIdResponseMapper : Profile
{
    public GetUserByIdResponseMapper()
    {
        CreateMap<User, GetUserByIdResponse>();
    }
}