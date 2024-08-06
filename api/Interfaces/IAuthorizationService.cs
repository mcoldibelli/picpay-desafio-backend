namespace api.Interfaces;

public interface IAuthorizationService
{
    public Task<bool> GetAuthorizationAsync();

}
