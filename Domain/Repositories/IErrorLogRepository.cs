using Domain.Entities;

namespace Domain.Repositories;
public interface IErrorLogRepository
{
    Task AddAsync(ErrorLog errorLog);
}