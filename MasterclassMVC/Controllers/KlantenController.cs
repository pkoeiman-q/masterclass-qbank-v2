using Azure;
using MasterclassApiTest.Entities;
using MasterclassApiTest.Models;
using MasterclassApiTest.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MasterclassMVC.Controllers
{
    [Authorize]
    public class KlantenController : Controller
    {
        private Uri _klantUri = new Uri("https://localhost:7267/api/klanten");
        private HttpClient _apiClient;
        private readonly IConfiguration _config;

        public KlantenController(IConfiguration config)
        {
            _config = config;
            _apiClient = new HttpClient();
            _apiClient.DefaultRequestHeaders.Add("X-Api-Version", "2.0");
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_config.GetValue<string>("ApiJwtToken")}");
        }

        public async Task<IActionResult> Index()
        {
            var klanten = await _apiClient.GetFromJsonAsync<List<GetKlantDTO>>(_klantUri);
            return View(klanten);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateKlantDTO createKlantDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(createKlantDTO);
            }

            var response = await _apiClient.PostAsJsonAsync(_klantUri, createKlantDTO);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Error");
        }

        [HttpGet]
        [Route("/klanten/update/{klantId}")]
        // TODO: fix it so the update page can be loaded even if the Redis cache is down
        public async Task<IActionResult> Update(int klantId)
        {
            GetKlantDTO? klant = await _apiClient.GetFromJsonAsync<GetKlantDTO>($"{_klantUri}/{klantId}");
            if (klant == null) throw new NullReferenceException();
            return View(klant);
        }

        [HttpPost]
        public async Task<IActionResult> Update(GetKlantDTO getKlantDTO)
        {
            var klantId = getKlantDTO.Id;
            var response = await _apiClient.PutAsJsonAsync($"{_klantUri}/{klantId}/mvc", getKlantDTO);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Error");
        }

        /*
            var request = await _apiClient.GetAsync(_klantUri);
            var body = request.Content.ReadAsStringAsync();
            var message = JsonConvert.DeserializeObject(body.Result);
        */
    }
}
