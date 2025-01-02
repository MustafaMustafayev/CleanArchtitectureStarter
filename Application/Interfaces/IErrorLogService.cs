using Application.Dtos.ErrorLogs;

namespace Application.Interfaces;
public interface IErrorLogService
{
    Task AddAsync(ErrorLogCreateDto dto, CancellationToken cancellationToken);
}