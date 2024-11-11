using Application.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Users.Queries.GetById;
public sealed class GetUserByIdQueryHandler(IMapper mapper,
                                            IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, IDataResult<GetUserByIdResponse>>
{
    public async Task<IDataResult<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        User user = (await userRepository.GetAsNoTrackingAsync(m => m.Id == request.Id))!;

        GetUserByIdResponse dto = mapper.Map<GetUserByIdResponse>(user);

        return new SuccessDataResult<GetUserByIdResponse>(dto);
    }
}