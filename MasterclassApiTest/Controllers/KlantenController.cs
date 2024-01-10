using MasterclassApiTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using System.Numerics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterclassApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KlantenController : ControllerBase
    {
        static List<Klant> klanten = new List<Klant>();
        private bool KlantIdExists(int id)
        {
            foreach (var klant in klanten)
            {
                if (klant.klantNummer == id)
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
                klantNummer = klanten.Count(),
                loginNaam = klant.loginNaam,
                laatstIngelogd = klant.laatstIngelogd,
                displayNaam = klant.displayNaam,
                voorletters = klant.voorletters,
                achternaam = klant.achternaam,
                geslacht = klant.geslacht,
                geboorteDatum = klant.geboorteDatum,
                overlijdensDatum = klant.overlijdensDatum,
                straat = klant.straat,
                huisnummer = klant.huisnummer,
                huisnummerToevoeging = klant.huisnummerToevoeging,
                postcode = klant.postcode,
                woonplaats = klant.woonplaats,
                bsn = klant.bsn,
                telefoonNummer = klant.telefoonNummer,
                email = klant.email,
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
        public IActionResult Post([FromBody] Klant klant)
        {
            if (klant == null)
            {
                return BadRequest(ModelState);
            }
            Klant newKlant = CreateNewKlant(klant);
            
            return Ok(newKlant);
        }

        // PUT api/<KlantenController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Klant klant)
        {
            if (klant == null || !KlantIdExists(id))
            {
                return BadRequest(ModelState);
            }
            var existingKlant = klanten[id] as Klant;

            // Verwijder de oude klant en voeg de bijgewerkte versie toe
            klanten.Remove(existingKlant);
            Klant newKlant = CreateNewKlant(klant);

            return Ok(newKlant);
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
        public IActionResult KlantRekeningen(int id)
        {
            // TODO: implement
            return Ok();
        }

        [HttpGet]
        [Route("{klantId}/rekeningen/{rekeningId}")]
        public IActionResult KlantRekening(int klantId, int rekeningId)
        {
            // TODO: implement
            return Ok();
        }
    }
}
