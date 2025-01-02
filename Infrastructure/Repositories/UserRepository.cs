using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public sealed class UserRepository(AppDbContext dbContext) : GenericRepository<User>(dbContext), IUserRepository
{
    public async Task<bool> IsEmailExistAsync(Guid? id, string email)
    {
        return await dbContext.Users.AnyAsync(m => m.Id != id && m.Email == email);
    }

    public new async Task UpdateAsync(User user)
    {
        dbContext.Entry(user).State = EntityState.Modified;

        dbContext.Entry(user).Property(m => m.PasswordSalt).IsModified = false;
        dbContext.Entry(user).Property(m => m.PasswordHash).IsModified = false;

        await Task.CompletedTask;
    }
}