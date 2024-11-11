namespace Domain.Repositories;
public interface IUserRepository
{
    Task<bool> IsEmailExistAsync(Guid? id, string email);
}