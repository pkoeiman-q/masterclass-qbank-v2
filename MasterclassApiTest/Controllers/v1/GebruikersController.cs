﻿using MasterclassApiTest.Entities;
using MasterclassApiTest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
                var role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value;
                var parsedRole = Enum.TryParse(role, true, out UserAccessRole userAccessRole);
                return new Gebruiker
                {
                    LoginNaam = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    Role = userAccessRole
                };
            }
            return null;
        }
    }
}
