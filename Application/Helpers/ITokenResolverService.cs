namespace Application.Helpers;
public interface ITokenResolverService
{
    public Guid? GetUserIdFromToken();
}