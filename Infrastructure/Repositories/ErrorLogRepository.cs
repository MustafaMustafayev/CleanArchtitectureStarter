using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DatabaseContexts;

namespace Infrastructure.Repositories;
public sealed class ErrorLogRepository(AppDbContext dbContext) : GenericRepository<ErrorLog>(dbContext), IErrorLogRepository
{
}
