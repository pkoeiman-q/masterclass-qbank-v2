using MasterclassApiTest.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterclassApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RekeningenController : ControllerBase
    {
        static List<Rekening> rekeningen = new List<Rekening>();

        private int RekeningFindIndexById(string id)
        {
            var index = 0;
            foreach (var rekening in rekeningen)
            {
                if (rekening.rekeningNummer == id)
                {
                    return index;
                }
                index++;
            }

            return -1;
        }

        private Rekening CreateNewRekening(Rekening rekening)
        {
            Rekening newRekening = new Rekening
            {
                rekeningNummer = rekening.rekeningNummer,
                type = rekening.type,
                saldo = rekening.saldo,
                status = rekening.status,
                beginDatum = rekening.beginDatum
            };
            rekeningen.Add(newRekening);
            return newRekening;
        }

        // GET: api/<RekeningController>
        [HttpGet]
        public IActionResult Get()
        {
            if (rekeningen.Count == 0)
            {
                return Ok("Geen rekeningen gevonden (lijstgrootte is 0)");
            }

            return Ok(rekeningen);
        }

        // GET api/<RekeningController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var listIndex = RekeningFindIndexById(id);
            if (listIndex == -1)
            {
                return BadRequest("De gegeven ID is niet gevonden");
            }

            Rekening rekening = rekeningen.ElementAt(listIndex);
            return Ok(rekening);
        }

        // POST api/<RekeningController>
        [HttpPost]
        public IActionResult Post([FromBody] Rekening rekening)
        {
            Rekening newRekening = CreateNewRekening(rekening);
            return Ok(rekening);
        }

        // PUT api/<RekeningController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RekeningController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
