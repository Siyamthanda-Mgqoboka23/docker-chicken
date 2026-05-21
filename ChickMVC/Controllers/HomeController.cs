using ChickMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChickMVC.Controllers
{

    public class HomeController : Controller
    {
   private readonly HttpClient _httpClient;

    public HomeController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IActionResult> Index()
    {
        try {
            var response = await _httpClient.GetStringAsync("http://localhost:5232/api/chickens");
            ViewBag.Chickens = response;
        }
        catch (Exception ex)
        {
            // Handle error (e.g., log it)
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
}
