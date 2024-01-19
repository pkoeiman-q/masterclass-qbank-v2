using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using System.Numerics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using MasterclassApiTest.Entities;
using MasterclassApiTest.Services;
using MasterclassApiTest.Models;
using MasterclassApiTest.Pagination;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterclassApiTest.Controllers.v2
{
    // [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class KlantenController : ControllerBase
    {
        private readonly KlantenService _service;
        public KlantenController(KlantenService service)
        {
            _service = service;
        }

        // GET: api/<KlantenController>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetKlantDTO>), 200)]
        public async Task<IActionResult> Get([FromQuery] KlantPageParameters klantPageParameters)
        {
            List<GetKlantDTO> klanten = await _service.GetAllKlanten(klantPageParameters);
            return Ok(klanten);
        }

        // GET api/<KlantenController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetKlantDTO), 200)]
        public async Task<IActionResult> Get(int id)
        {
            GetKlantDTO? klant = await _service.GetKlant(id);
            if (klant == null) return KlantNotFoundMessage();
            return Ok(klant);
        }

        // POST api/<KlantenController>
        [HttpPost]
        [ProducesResponseType(typeof(GetKlantDTO), 200)]
        public async Task <IActionResult> Post([FromBody] CreateKlantDTO input)
        {
            GetKlantDTO? klant = await _service.CreateKlant(input);
            if (klant == null)
            {
                return StatusCode(500, "Iets is misgegaan tijdens het aanmaken van de klant.");
            }
            return Ok(klant);
        }

        // PUT api/<KlantenController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(GetKlantDTO), 200)]
        public async Task<IActionResult> Put(int id, [FromBody] CreateKlantDTO input)
        {
            GetKlantDTO? klant = await _service.UpdateKlant(id, input);
            if (klant == null) return KlantNotFoundMessage();
            return Ok(klant);
        }

        // DELETE api/<KlantenController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(GetKlantDTO), 200)]
        public async Task<IActionResult> Delete(int id)
        {
            GetKlantDTO? klant = await _service.DeleteKlant(id);
            if (klant == null) return KlantNotFoundMessage();
            return Ok(klant);
        }

        private IActionResult KlantNotFoundMessage()
        {
            return NotFound("De klant met de gegeven ID is niet gevonden.");
        }

        [HttpGet]
        [Route("{id}/rekeningen")]
        public IActionResult Rekeningen(int id)
        {
            // TODO: implement
            return Ok("TODO: laat alle rekeningen van de gegeven klant zien.");
        }

        [HttpGet]
        [Route("{klantId}/rekeningen/{rekeningId}")]
        public IActionResult Rekeningen(int klantId, string rekeningId)
        {
            // TODO: implement
            return Ok("TODO: laat 1 rekening van de gegeven klant zien.");
        }

        [HttpPost]
        [Route("{id}/rekeningen")]
        public IActionResult CreateRekening(string id)
        {
            // TODO: implement
            return Ok("TODO: maak een nieuwe rekening voor de gegeven klant aan.");
        }

        //[HttpGet]
        //[Route("zoeken")]
        //public IActionResult ZoekKlant([FromQuery] string searchType, [FromQuery] string searchTerm)
        //{
        //    List<Klant> gevondenKlanten = Klant.ZoekKlant(klanten, searchType, searchTerm);
        //    if (gevondenKlanten.Count == 0)
        //    {
        //        return Ok($"De gegeven klant is niet gevonden. Zoekterm = \"{searchTerm}\"");
        //    }
        //    return Ok(gevondenKlanten);
        //}
    }
}
