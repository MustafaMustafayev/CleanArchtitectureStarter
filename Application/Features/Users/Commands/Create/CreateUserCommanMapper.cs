using AutoMapper;
using Domain.Entities;

namespace Application.Features.Users.Commands.Create;
public class CreateUserCommanMapper : Profile
{
    public CreateUserCommanMapper()
    {
        CreateMap<CreateUserCommand, User>();
    }
}
