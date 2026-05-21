using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChickenMVC.Models;

namespace ChickenMVC.Controllers;

public class HomeController : Controller
{

    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<HomeController> _logger;

    public HomeController(HttpClient httpClient, IConfiguration configuration, ILogger<HomeController> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var apiBaseUrl = _configuration["ChickenApi:BaseUrl"] ?? "http://localhost:5232";
        var isRunningInContainer = string.Equals(
            Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"),
            "true",
            StringComparison.OrdinalIgnoreCase);

        if (isRunningInContainer && apiBaseUrl.Contains("localhost", StringComparison.OrdinalIgnoreCase))
        {
            apiBaseUrl = "http://chickenapi:8080";
        }

        var explicitApiBaseUrl = Environment.GetEnvironmentVariable("CHICKEN_API_BASE_URL");
        if (!string.IsNullOrWhiteSpace(explicitApiBaseUrl))
        {
            apiBaseUrl = explicitApiBaseUrl;
        }

        var chickensEndpoint = $"{apiBaseUrl.TrimEnd('/')}/api/chickens";
        _logger.LogInformation("Fetching chickens from {Endpoint}", chickensEndpoint);

        try
        {
            var response = await _httpClient.GetStringAsync(chickensEndpoint);
            ViewBag.Chickens = response;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed fetching chickens from {Endpoint}", chickensEndpoint);
            ViewBag.Chickens = $"Error fetching chickens: {ex.Message}";
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
