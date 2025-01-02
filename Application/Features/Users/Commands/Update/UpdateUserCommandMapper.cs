using AutoMapper;
using Domain.Entities;

namespace Application.Features.Users.Commands.Update;
public sealed class UpdateUserCommandMapper : Profile
{
    public UpdateUserCommandMapper()
    {
        CreateMap<UpdateUserCommand, User>();
    }
}