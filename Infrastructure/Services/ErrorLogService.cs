using Application.Dtos.ErrorLogs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Services;
public sealed class ErrorLogService(IErrorLogRepository errorLogRepository,
                                    IUnitOfWork unitOfWork) : IErrorLogService
{
    public async Task AddAsync(ErrorLogCreateDto dto, CancellationToken cancellationToken)
    {      
          ErrorLog data = new()
          {
              AccessToken = dto.AccessToken!,
              ErrorMessage = dto.ErrorMessage!,
              Ip = dto.Ip!,
              Path = dto.Path!,
              StackTrace = dto.StackTrace!,
              UserId = dto.UserId,
              TraceIdentifier = dto.TraceIdentifier!
          };

          await errorLogRepository.AddAsync(data);
          await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
