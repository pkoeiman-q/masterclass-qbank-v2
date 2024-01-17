using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using System.Numerics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using MasterclassApiTest.Entities;
using MasterclassApiTest.Services;

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
        List<Klant> klanten = new List<Klant>();

        private bool KlantIdExists(int id)
        {
            foreach (var klant in klanten)
            {
                if (klant.KlantNummer == id)
                {
                    return true;
                }
            }

            return false;
        }

        // GET: api/<KlantenController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Klant> klanten = await _service.GetAllKlanten();
            return Ok(klanten);
        }

        // GET api/<KlantenController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!KlantIdExists(id))
            {
                return NotFound("De gegeven ID is niet gevonden");
            }

            Klant klant = klanten.ElementAt(id);
            return Ok(klant);
        }

        // POST api/<KlantenController>
        [HttpPost]
        [ProducesResponseType(typeof(Klant), 200)]
        public IActionResult Post([FromBody] Klant klant)
        {
            throw new NotImplementedException();
        }

        // PUT api/<KlantenController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Klant klant)
        {
            if (!KlantIdExists(id))
            {
                return BadRequest("Geprobeerd een klant aan te passen, maar deze was niet gevonden.");
            }
            klanten[id] = klant;

            return Ok(klanten[id]);
        }

        // DELETE api/<KlantenController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!KlantIdExists(id))
            {
                return BadRequest("De gegeven ID is niet gevonden");
            }

            var existingKlant = klanten[id] as Klant;
            klanten.Remove(existingKlant);

            return Ok("De klant met de gegeven ID is verwijderd");
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
