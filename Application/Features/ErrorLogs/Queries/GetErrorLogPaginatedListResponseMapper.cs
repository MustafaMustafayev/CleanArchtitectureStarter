using AutoMapper;
using Domain.Entities;

namespace Application.Features.ErrorLogs.Queries;
internal sealed class GetErrorLogPaginatedListResponseMapper : Profile
{
    public GetErrorLogPaginatedListResponseMapper()
    {
        CreateMap<ErrorLog, GetErrorLogPaginatedListResponse>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd/MM/yyyy HH:mm")));
    }
}