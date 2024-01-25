using Azure;
using MasterclassApiTest.Entities;
using MasterclassApiTest.Models;
using MasterclassApiTest.Pagination;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MasterclassMVC.Controllers
{
    public class KlantenController : Controller
    {
        private Uri _baseAdress = new Uri("https://localhost:7267/api/klanten");
        private HttpClient _apiClient;

        public KlantenController()
        {
            _apiClient = new HttpClient();
            _apiClient.BaseAddress = _baseAdress;
            _apiClient.DefaultRequestHeaders.Add("X-Api-Version", "2.0");
        }

        public async Task<IActionResult> Index()
        {
            var klanten = await _apiClient.GetFromJsonAsync<List<GetKlantDTO>>(_baseAdress);
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
            var response = await _apiClient.PostAsJsonAsync(_baseAdress, createKlantDTO);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Error");
            // var body = await response.Content.ReadAsStringAsync();
            // GetKlantDTO? getKlantDTO = JsonConvert.DeserializeObject<GetKlantDTO>(body);
        }

        [HttpGet]
        [Route("/klanten/update/{klantId}")]
        public async Task<IActionResult> Update(int klantId)
        {
            GetKlantDTO? klant = await _apiClient.GetFromJsonAsync<GetKlantDTO>($"{_baseAdress}/{klantId}");
            if (klant == null) throw new NullReferenceException();
            return View(klant);
        }

        // TODO: View data aan de view meegeven zodat direct vanuit de form naar de API data verzonden wordt
        // Vraag hierbij: hoe werkt dat als je authenticatie wil toevoegen?
        //ViewData["FormActionUrl"] = _baseAdress;

        [HttpPost]
        public async Task<IActionResult> Update(GetKlantDTO getKlantDTO)
        {
            var klantId = getKlantDTO.Id;
            var response = await _apiClient.PutAsJsonAsync($"{_baseAdress}/{klantId}/mvc", getKlantDTO);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Error");
        }
    }
}
