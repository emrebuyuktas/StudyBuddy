using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace StudyBuddy.WebUi.Utils;

public class AuthStateProvider: AuthenticationStateProvider
{

    private readonly ILocalStorageService _localStorageService;
    private readonly AuthenticationState _anonymous;
    public AuthStateProvider(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
        _anonymous = new AuthenticationState(new System.Security.Claims.ClaimsPrincipal(new ClaimsIdentity()));

    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        String apiToken = await _localStorageService.GetItemAsStringAsync("token");

        if (String.IsNullOrEmpty(apiToken))
        {
            return _anonymous;
        }

        String email = await _localStorageService.GetItemAsStringAsync("email");
        var cp = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) }, "jwtAuthType"));
        return new AuthenticationState(cp);
    }

    public void NotifyUserLogin(String email)
    {
        var cp = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) }, "jwtAuthType"));
        var authState=Task.FromResult(new AuthenticationState(cp));
        NotifyAuthenticationStateChanged(authState);
    }
    public void NotifyUserLogout(String email)
    {
        var authState=Task.FromResult(_anonymous);

        NotifyAuthenticationStateChanged(authState);

    }
}