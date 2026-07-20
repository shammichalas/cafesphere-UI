using System.Net.Http.Json;
using Microsoft.AspNetCore.SignalR.Client;
using UI.Models;

namespace UI.Services;

public class ApiDashboardMetrics
{
    public decimal TodayRevenue { get; set; }
    public int TodayOrdersCount { get; set; }
    public decimal MonthRevenue { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal NetProfit { get; set; }
    public int ActiveTablesCount { get; set; }
    public int PendingKitchenOrdersCount { get; set; }
    public List<ApiTopProduct> TopProducts { get; set; } = new();
}

public class ApiTopProduct
{
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int QuantitySold { get; set; }
    public decimal TotalRevenue { get; set; }
}

public class ApiProduct
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal CostPrice { get; set; }
    public string CategoryId { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int PreparationTimeMinutes { get; set; }
    public bool IsAvailable { get; set; }
}

public class ApiCategory
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}

public class ApiOrder
{
    public string Id { get; set; } = string.Empty;
    public string OrderNumber { get; set; } = string.Empty;
    public string? CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public int Type { get; set; }
    public int Status { get; set; }
    public string? TableId { get; set; }
    public string? TableNumber { get; set; }
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Notes { get; set; }
    public List<ApiOrderItem> Items { get; set; } = new();
    public int PaymentStatus { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ApiOrderItem
{
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal SubTotal { get; set; }
    public string? SpecialInstructions { get; set; }
}

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly AuthService _authService;

    public ApiService(HttpClient httpClient, AuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    private void EnsureAuthHeader()
    {
        if (_authService.IsAuthenticated && _authService.CurrentUser != null)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _authService.CurrentUser.AccessToken);
        }
    }

    public async Task<List<ApiProduct>> GetProductsAsync(string? categoryId = null)
    {
        try
        {
            EnsureAuthHeader();
            var url = string.IsNullOrWhiteSpace(categoryId) ? "api/v1/products" : $"api/v1/products?categoryId={categoryId}";
            var items = await _httpClient.GetFromJsonAsync<List<ApiProduct>>(url);
            return items ?? new List<ApiProduct>();
        }
        catch
        {
            return new List<ApiProduct>();
        }
    }

    public async Task<List<ApiCategory>> GetCategoriesAsync()
    {
        try
        {
            EnsureAuthHeader();
            var items = await _httpClient.GetFromJsonAsync<List<ApiCategory>>("api/v1/categories");
            return items ?? new List<ApiCategory>();
        }
        catch
        {
            return new List<ApiCategory>();
        }
    }

    public async Task<ApiDashboardMetrics?> GetDashboardMetricsAsync()
    {
        try
        {
            EnsureAuthHeader();
            return await _httpClient.GetFromJsonAsync<ApiDashboardMetrics>("api/v1/dashboard/metrics");
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<ApiOrder>> GetKitchenQueueAsync()
    {
        try
        {
            EnsureAuthHeader();
            var items = await _httpClient.GetFromJsonAsync<List<ApiOrder>>("api/v1/kitchen/queue");
            return items ?? new List<ApiOrder>();
        }
        catch
        {
            return new List<ApiOrder>();
        }
    }

    public async Task<bool> UpdateKitchenStatusAsync(string orderId, int newStatus)
    {
        try
        {
            EnsureAuthHeader();
            var response = await _httpClient.PatchAsJsonAsync($"api/v1/kitchen/orders/{orderId}/status", new { NewStatus = newStatus });
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<ApiOrder?> CreateOrderAsync(string customerName, int orderType, List<object> items, string? notes = null)
    {
        try
        {
            EnsureAuthHeader();
            var payload = new
            {
                CustomerName = customerName,
                Type = orderType,
                Notes = notes,
                Items = items
            };
            var response = await _httpClient.PostAsJsonAsync("api/v1/orders", payload);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApiOrder>();
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    public HubConnection CreateHubConnection(string hubPath)
    {
        var baseUri = _httpClient.BaseAddress ?? new Uri("http://localhost:5000/");
        var hubUrl = new Uri(baseUri, hubPath);

        return new HubConnectionBuilder()
            .WithUrl(hubUrl, options =>
            {
                if (_authService.IsAuthenticated && _authService.CurrentUser != null)
                {
                    options.AccessTokenProvider = () => Task.FromResult<string?>(_authService.CurrentUser.AccessToken);
                }
            })
            .WithAutomaticReconnect()
            .Build();
    }
}
