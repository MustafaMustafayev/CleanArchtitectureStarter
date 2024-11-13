using AutoMapper;
using Domain.Entities;

namespace Application.Dtos.Users;
public sealed class UserLoginInfoDtoMapper : Profile
{
    public UserLoginInfoDtoMapper()
    {
        CreateMap<User, UserLoginInfoDto>(); 
    }
}
