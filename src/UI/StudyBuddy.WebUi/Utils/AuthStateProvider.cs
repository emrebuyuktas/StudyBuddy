using Microsoft.AspNetCore.Components.Authorization;

namespace StudyBuddy.WebUi.Utils;

public class AuthStateProvider: AuthenticationStateProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        throw new NotImplementedException();
    }
}