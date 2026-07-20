using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace UI.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly AuthService _authService;

    public CustomAuthStateProvider(AuthService authService)
    {
        _authService = authService;
        _authService.OnAuthStateChanged += NotifyAuthenticationStateChangedInternal;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal principal;

        if (_authService.IsAuthenticated && _authService.CurrentUser != null)
        {
            var user = _authService.CurrentUser;
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.UserId ?? string.Empty),
                new(ClaimTypes.Name, !string.IsNullOrEmpty(user.FullName) ? user.FullName : user.Username),
                new(ClaimTypes.Email, user.Email ?? string.Empty),
                new(ClaimTypes.Role, user.Role ?? string.Empty)
            };

            var identity = new ClaimsIdentity(claims, "jwt");
            principal = new ClaimsPrincipal(identity);
        }
        else
        {
            principal = new ClaimsPrincipal(new ClaimsIdentity());
        }

        return Task.FromResult(new AuthenticationState(principal));
    }

    private void NotifyAuthenticationStateChangedInternal()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
