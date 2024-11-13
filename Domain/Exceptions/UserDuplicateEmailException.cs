namespace Domain.Exceptions;
public sealed class UserDuplicateEmailException(string email) : Exception($"User with the {email} is exist")
{
}