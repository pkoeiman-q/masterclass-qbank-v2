using MasterclassApiTest.Data;
using MasterclassApiTest.Entities;
using MasterclassApiTest.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterclassApiTest.Controllers.v2
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class RekeningenController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public RekeningenController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [ProducesResponseType(typeof(RekeningDTO), 200)]
        public async Task<IActionResult> Post([FromBody] RekeningDTO rekeningDTO)
        {
            RekeningDTO? rekening = await _unitOfWork.Rekeningen.CreateRekening(rekeningDTO.KlantNummer, rekeningDTO);
            if (rekening == null)
            {
                return StatusCode(500, "Iets is verkeerd gegaan tijdens het aanmaken van de rekening.");
            }
            await _unitOfWork.Complete();
            return Ok(rekening);
        }

        [HttpPut]
        [ProducesResponseType(typeof(RekeningDTO), 200)]
        public async Task<IActionResult> Put([FromBody] RekeningDTO rekeningDTO)
        {
            RekeningDTO? rekening = await _unitOfWork.Rekeningen.UpdateRekening(rekeningDTO);
            if (rekening == null)
            {
                return StatusCode(500, "Iets is verkeerd gegaan tijdens het bijwerken van de rekening.");
            }
            await _unitOfWork.Complete();
            return Ok(rekening);
        }
    }
}
