using Application.Dtos.ErrorLog;

namespace Application.Interfaces;
public interface IErrorLogService
{
    Task AddAsync(ErrorLogCreateDto dto, CancellationToken cancellationToken);
}
