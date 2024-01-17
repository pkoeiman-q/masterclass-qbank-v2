using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using System.Numerics;
using System.Linq;
using MasterclassApiTest.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterclassApiTest.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class KlantenController : ControllerBase
    {
        static List<Klant> klanten = new List<Klant>();

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
        private Klant CreateNewKlant(Klant klant)
        {
            Klant newKlant = new Klant
            {
                KlantNummer = klanten.Count(),
                LoginNaam = klant.LoginNaam,
                LaatstIngelogd = klant.LaatstIngelogd,
                DisplayNaam = klant.DisplayNaam,
                Voorletters = klant.Voorletters,
                Achternaam = klant.Achternaam,
                Geslacht = klant.Geslacht,
                GeboorteDatum = klant.GeboorteDatum,
                OverlijdensDatum = klant.OverlijdensDatum,
                Straat = klant.Straat,
                Huisnummer = klant.Huisnummer,
                HuisnummerToevoeging = klant.HuisnummerToevoeging,
                Postcode = klant.Postcode,
                Woonplaats = klant.Woonplaats,
                Bsn = klant.Bsn,
                TelefoonNummer = klant.TelefoonNummer,
                Email = klant.Email,
            };
            klanten.Add(newKlant);
            return newKlant;
        }

        // GET: api/<KlantenController>
        [HttpGet]
        public IActionResult Get()
        {
            if (klanten.Count == 0)
            {
                return Ok("Geen klanten gevonden (lijstgrootte is 0)");
            }

            return Ok(klanten);
        }

        // GET api/<KlantenController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!KlantIdExists(id))
            {
                return BadRequest("De gegeven ID is niet gevonden");
            }

            Klant klant = klanten.ElementAt(id);
            return Ok(klant);
        }

        // POST api/<KlantenController>
        [HttpPost]
        [ProducesResponseType(typeof(Klant), 200)]
        public IActionResult Post([FromBody] Klant klant)
        {
            Klant newKlant = CreateNewKlant(klant);
            return Ok(newKlant);
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
    }
}
