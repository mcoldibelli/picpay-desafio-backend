using api.Interfaces;
using api.Models;

namespace api.Services.External;

public class AuthorizationService : IAuthorizationService
{
    private readonly HttpClient _httpClient;
    private readonly string? _uriAuthorization;

    public AuthorizationService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _uriAuthorization = configuration.GetValue<string>("externalServices:authorization");

    }

    public async Task<bool> GetAuthorizationAsync()
    {
        var response = await  _httpClient
            .GetFromJsonAsync<AuthorizationResponse>(_uriAuthorization);
        return response?.Data?.Authorization ?? false;
    }
}
