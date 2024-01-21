using Azure.Core;
using MasterclassMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MasterclassMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private Uri _baseAdress = new Uri("https://localhost:7267/api");
        private HttpClient _apiClient;

        public HomeController(ILogger<HomeController> logger)
        {
            _apiClient = new HttpClient();
            _apiClient.BaseAddress = _baseAdress;
            _apiClient.DefaultRequestHeaders.Add("Accept", "application/json;x-api-version=2.0");
            
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var klanten = await _apiClient.GetAsync($"{_baseAdress}/klanten");
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
