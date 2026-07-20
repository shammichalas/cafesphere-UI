using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.JSInterop;

namespace UI.Services;

public class AuthResponseModel
{
    public string UserId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime AccessTokenExpiresAt { get; set; }
}

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;
    private const string AuthStorageKey = "cafesphere_auth_user";

    public event Action? OnAuthStateChanged;

    public AuthResponseModel? CurrentUser { get; private set; }
    public bool IsAuthenticated => CurrentUser != null && !string.IsNullOrEmpty(CurrentUser.AccessToken);

    public AuthService(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }

    public async Task InitializeAsync()
    {
        try
        {
            var storedJson = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", AuthStorageKey);
            if (!string.IsNullOrEmpty(storedJson))
            {
                CurrentUser = JsonSerializer.Deserialize<AuthResponseModel>(storedJson);
                if (CurrentUser != null && !string.IsNullOrEmpty(CurrentUser.AccessToken))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = 
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CurrentUser.AccessToken);
                }
            }
        }
        catch
        {
            CurrentUser = null;
        }
        OnAuthStateChanged?.Invoke();
    }

    public async Task<(bool Success, string Message)> LoginAsync(string emailOrUsername, string password)
    {
        try
        {
            var request = new { EmailOrUsername = emailOrUsername, Password = password };
            var response = await _httpClient.PostAsJsonAsync("api/v1/auth/login", request);

            if (response.IsSuccessStatusCode)
            {
                var authResult = await response.Content.ReadFromJsonAsync<AuthResponseModel>();
                if (authResult != null)
                {
                    CurrentUser = authResult;
                    var json = JsonSerializer.Serialize(authResult);
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", AuthStorageKey, json);
                    
                    _httpClient.DefaultRequestHeaders.Authorization = 
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CurrentUser.AccessToken);

                    OnAuthStateChanged?.Invoke();
                    return (true, "Login successful!");
                }
            }

            var errorResponse = await response.Content.ReadAsStringAsync();
            return (false, "Invalid login credentials. Please check your email/username and password.");
        }
        catch (Exception ex)
        {
            return (false, $"Connection error: {ex.Message}");
        }
    }

    public async Task<(bool Success, string Message)> RegisterAsync(string username, string email, string password, string fullName, string? phoneNumber)
    {
        try
        {
            var request = new 
            { 
                Username = username, 
                Email = email, 
                Password = password, 
                FullName = fullName, 
                PhoneNumber = phoneNumber 
            };

            var response = await _httpClient.PostAsJsonAsync("api/v1/auth/register", request);

            if (response.IsSuccessStatusCode)
            {
                var authResult = await response.Content.ReadFromJsonAsync<AuthResponseModel>();
                if (authResult != null)
                {
                    CurrentUser = authResult;
                    var json = JsonSerializer.Serialize(authResult);
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", AuthStorageKey, json);
                    
                    _httpClient.DefaultRequestHeaders.Authorization = 
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CurrentUser.AccessToken);

                    OnAuthStateChanged?.Invoke();
                    return (true, "Registration successful!");
                }
            }

            return (false, "Registration failed. User with this email or username may already exist.");
        }
        catch (Exception ex)
        {
            return (false, $"Connection error: {ex.Message}");
        }
    }

    public async Task LogoutAsync()
    {
        CurrentUser = null;
        _httpClient.DefaultRequestHeaders.Authorization = null;
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", AuthStorageKey);
        OnAuthStateChanged?.Invoke();
    }
}
