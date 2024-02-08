using MasterclassApiTest.Data;
using MasterclassApiTest.Entities;
using MasterclassApiTest.Exceptions;
using MasterclassApiTest.Models;
using MasterclassApiTest.Pagination;
using MasterclassApiTest.RabbitMQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterclassApiTest.Controllers.v2
{
    // [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    [Authorize]
    public class KlantenController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public KlantenController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<KlantenController>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetKlantDTO>), 200)]
        public async Task<IActionResult> Get([FromQuery] KlantPageParameters klantPageParameters)
        {
            List<GetKlantDTO> klanten = await _unitOfWork.Klanten.GetAllKlanten(klantPageParameters);

            return Ok(klanten);
        }

        // GET api/<KlantenController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetKlantDTO), 200)]
        public async Task<IActionResult> Get(int id)
        {
            GetKlantDTO? klant = await _unitOfWork.Klanten.GetKlant(id);
            if (klant == null) throw new KlantNotFoundException(id);

            Log.Information("Opgevraagde klant: {@klant}", klant);
            return Ok(klant);
        }

        // POST api/<KlantenController>
        [HttpPost]
        [ProducesResponseType(typeof(GetKlantDTO), 200)]
        public async Task<IActionResult> Post([FromBody] CreateKlantDTO input)
        {
            GetKlantDTO? klant = await _unitOfWork.Klanten.CreateKlant(input);
            if (klant == null) return StatusCode(500, "Iets is misgegaan tijdens het aanmaken van de klant.");

            await _unitOfWork.Complete();

            var prod = new RbMessageProducer();
            prod.ProduceMessage($"Created a new klant with displayname '{input.DisplayNaam}'");

            return Ok(klant);
        }

        // PUT api/<KlantenController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(GetKlantDTO), 200)]
        public async Task<IActionResult> Put(int id, [FromBody] CreateKlantDTO input)
        {
            GetKlantDTO? klant = await _unitOfWork.Klanten.UpdateKlant(id, input);
            if (klant == null) throw new KlantNotFoundException(id);

            await _unitOfWork.Complete();

            var prod = new RbMessageProducer();
            prod.ProduceMessage($"Updated data for klant '{input.DisplayNaam}' (ID {id})");

            return Ok(klant);
        }

        [HttpPut]
        [Route("{klantId}/mvc")]
        [ProducesResponseType(typeof(GetKlantDTO), 200)]
        public async Task<IActionResult> Put(int klantId, [FromBody] GetKlantDTO input)
        {
            GetKlantDTO? klant = await _unitOfWork.Klanten.UpdateKlant(klantId, input);
            if (klant == null) throw new KlantNotFoundException(klantId);

            await _unitOfWork.Complete();
            return Ok(klant);
        }

        // DELETE api/<KlantenController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(GetKlantDTO), 200)]
        public async Task<IActionResult> Delete(int id)
        {
            GetKlantDTO? klant = await _unitOfWork.Klanten.DeleteKlant(id);
            if (klant == null) throw new KlantNotFoundException(id);

            await _unitOfWork.Complete();
            return Ok(klant);
        }

        private IActionResult KlantNotFoundMessage()
        {
            return NotFound("De klant met de gegeven ID is niet gevonden.");
        }

        [HttpGet]
        [Route("{klantId}/rekeningen")]
        public async Task<IActionResult> Rekeningen(int klantId)
        {
            List<RekeningDTO> rekeningen = await _unitOfWork.Rekeningen.GetRekeningen(klantId);
            if (rekeningen.Count == 0) throw new RekeningNotFoundException($"Geen rekeningen gevonden met de gegeven klant ID ({klantId}).");
            return Ok(rekeningen);
        }

        [HttpGet]
        [Route("{klantId}/rekeningen/{rekeningId}")]
        public async Task<IActionResult> Rekeningen(int klantId, string rekeningId)
        {
            RekeningDTO? rekening = await _unitOfWork.Rekeningen.GetSingleRekening(klantId, rekeningId);
            if (rekening == null) throw new RekeningNotFoundException(klantId, rekeningId);
            return Ok(rekening);
        }

        [HttpDelete]
        [Route("{klantId}/rekeningen/{rekeningId}")]
        public async Task<IActionResult> DeleteRekening(int klantId, string rekeningId)
        {
            RekeningDTO? rekening = await _unitOfWork.Rekeningen.DeleteRekening(klantId, rekeningId);
            if (rekening == null) throw new RekeningNotFoundException(klantId, rekeningId);
            await _unitOfWork.Complete();
            return Ok(rekening);
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
