namespace Application.Interfaces;
public interface ITokenResolverService
{
    public Guid? GetUserIdFromToken();
    public string GetAccessToken();
}