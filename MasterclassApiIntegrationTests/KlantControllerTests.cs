using System.Net.Http.Headers;
using System.Net;
using System.Net.Http.Json;
using MasterclassApiTest.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace MasterclassApiIntegrationTests
{
    public class KlantControllerTests
    {
        [Fact]
        public async Task KlantController_Call_Get_Method_Without_Authing()
        {
            // Arrange
            var app = new MasterclassApiWebApplicationFactory();
            var client = app.CreateClient();

            // Act
            var request = new HttpRequestMessage(HttpMethod.Get, "api/klanten");
            request.Headers.Add("Accept", "application/json;x-api-version=2.0");
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task KlantController_Call_Get_Method_While_Authed()
        {
            // Arrange
            var app = new MasterclassApiWebApplicationFactory();
            var client = app.CreateClient();
            var loginData = new GebruikerLogin
            {
                GebruikersNaam = "adam",
                Wachtwoord = "a"
            };

            // Act
            var jwtResponse = await client.PostAsJsonAsync("api/login", loginData);
            string token = await jwtResponse.Content.ReadAsStringAsync();

            var request = new HttpRequestMessage(HttpMethod.Get, "api/klanten");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("Accept", "application/json;x-api-version=2.0");
            var response = await client.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains("Geen klanten gevonden (lijstgrootte is 0)", responseBody);
        }
    }
}