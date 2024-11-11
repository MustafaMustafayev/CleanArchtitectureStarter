using Domain.Entities;
using System.Security;

namespace Domain.Repositories;
public interface IUserRepository : IGenericRepository<User>
{
    Task<bool> IsEmailExistAsync(Guid? id, string email);
    new Task UpdateAsync(User user);
}