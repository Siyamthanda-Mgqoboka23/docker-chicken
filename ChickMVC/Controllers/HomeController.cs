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
            try
            {
                var response = await _httpClient.GetStringAsync("http://localhost:5000/api/egg/GetMeEggs");
                ViewBag.Chicken = response;
            }
            catch (Exception ex)
            {
                ViewBag.Chicken = $"Error: {ex.Message}";
            } return View();
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
