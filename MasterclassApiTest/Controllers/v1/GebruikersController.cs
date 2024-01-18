using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using MasterclassApiTest.Entities;
using MasterclassApiTest.Services;

namespace MasterclassApiTest.Controllers.v1
{
    [Authorize(Roles = "Admin")]
    public class GebruikersController : ControllerBase
    {
        private readonly GebruikersService _service;
        public GebruikersController(GebruikersService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("api/[controller]/public")]
        [AllowAnonymous]
        public IActionResult Public()
        {
            return Ok("Hoi, dit is een publieke endpoint");
        }

        [HttpGet]
        [Route("api/[controller]/admin")]
        public IActionResult Admin()
        {
            var huidigeGebruiker = GetCurrentUser();
            return Ok($"Hoi {huidigeGebruiker.LoginNaam}, je rol is {huidigeGebruiker.Role}");
        }

        private Gebruiker GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new Gebruiker
                {
                    LoginNaam = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
    }
}
