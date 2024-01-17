using MasterclassApiTest.Entities;
using MasterclassApiTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MasterclassApiTest.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] GebruikerLogin login)
        {
            var gebruiker = Authenticate(login);

            if (gebruiker != null)
            {
                var token = Generate(gebruiker);
                return Ok(token);
            }

            return NotFound("Klant gebruiker niet gevonden");
        }

        private string Generate(Gebruiker gebruiker)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, gebruiker.GebruikersNaam),
                new Claim(ClaimTypes.Email, gebruiker.Email),
                new Claim(ClaimTypes.Role, gebruiker.Role)
            };

            var token = new JwtSecurityToken(
                _config["JwtSettings:Issuer"],
                _config["JwtSettings:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private Gebruiker? Authenticate(GebruikerLogin login)
        {
            // Vergelijk de gegeven login gegevens met die van een standaard gebruiker
            // DIT IS ALLEEN OM MEE TE TESTEN, DIT IS NIET VOOR PRODUCTIE GEBRUIK
            var gebruiker = GebruikerConstanten.Gebruikers.FirstOrDefault
            (
                o => o.GebruikersNaam.ToLower() == login.GebruikersNaam.ToLower()
                && o.Wachtwoord == login.Wachtwoord
            );

            if (gebruiker != null)
            {
                return gebruiker;
            }

            return null;
        }
    }
}
