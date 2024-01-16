using MasterclassApiTest.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MasterclassApiUnitTests
{
    public class KlantControllerTests
    {
        // Get all customers
        [Fact]
        public void KlantenControllerGet()
        {
            // Arrange
            var klantController = new MasterclassApiTest.Controllers.v2.KlantenController();

            // Act
            var result = klantController.Get();

            // Assert
            var okObject = Assert.IsType<OkObjectResult>(result);
            Assert.Contains("Geen klanten gevonden (lijstgrootte is 0)", (string) okObject.Value);
        }

        [Fact]
        public void KlantenControllerPost()
        {
            // Arrange
            var klantController = new MasterclassApiTest.Controllers.v2.KlantenController();

            // Act
            var klant = GenereerKlant();
            var result = klantController.Post(klant);

            // Assert
            var okObject = Assert.IsType<OkObjectResult>(result);
            var responseKlant = Assert.IsType<Klant>(okObject.Value);
            Assert.Equal(klant.LoginNaam, responseKlant.LoginNaam);
        }

        [Fact]
        public void KlantenControllerPostAndGetThePostedKlant()
        {
            // Arrange
            var klantController = new MasterclassApiTest.Controllers.v2.KlantenController();

            // Act
            var klant = GenereerKlant();
            klantController.Post(klant);
            var result = klantController.Get();

            // Assert
            var okObject = Assert.IsType<OkObjectResult>(result);
            var responseKlanten = Assert.IsType<List<Klant>>(okObject.Value);
            var responseKlant = responseKlanten.First();
            Assert.Single(responseKlanten);
            Assert.Equal(klant.LoginNaam, responseKlant.LoginNaam);
        }

        [Fact]
        public void KlantenControllerPostAndGetKlantById()
        {
            // Arrange
            var klantController = new MasterclassApiTest.Controllers.v2.KlantenController();

            // Act
            var klant = GenereerKlant();
            klantController.Post(klant);
            var found = klantController.Get(0);
            var notFound = klantController.Get(1);

            // Assert
            var okObject = Assert.IsType<OkObjectResult>(found);
            var responseKlant = Assert.IsType<Klant>(okObject.Value);
            Assert.Equal(klant.LoginNaam, responseKlant.LoginNaam);

            var notFoundObject = Assert.IsType<NotFoundObjectResult>(notFound);
            Assert.Contains("De gegeven ID is niet gevonden", (string) notFoundObject.Value);
        }

        [Fact]
        public void KlantenControllerPostAndDelete()
        {
            // Arrange
            var klantController = new MasterclassApiTest.Controllers.v2.KlantenController();

            // Act
            var klantA = GenereerKlant(0);
            var klantB = GenereerKlant(1);
            klantController.Post(klantA);
            klantController.Post(klantB);
            klantController.Delete(0);
            var result = klantController.Get();

            // Assert
            var okObject = Assert.IsType<OkObjectResult>(result);
            var responseKlanten = Assert.IsType<List<Klant>>(okObject.Value);
            var responseKlant = responseKlanten.First();
            Assert.Single(responseKlanten);
            Assert.Equal(klantB.KlantNummer, responseKlant.KlantNummer);
        }

        private Klant GenereerKlant(int id = 0)
        {
            Klant dummyKlant = new Klant
            {
                KlantNummer = id,
                LoginNaam = "login",
                LaatstIngelogd = DateTime.UnixEpoch,
                DisplayNaam = "display",
                Voorletters = "voor",
                Achternaam = "achter",
                Geslacht = "Man",
                GeboorteDatum = DateTime.UnixEpoch,
                OverlijdensDatum = null,
                Straat = "straat",
                Huisnummer = 1,
                HuisnummerToevoeging = null,
                Postcode = "1234AB",
                Woonplaats = "woonplaats",
                Bsn = 1,
                TelefoonNummer = "06-12345678",
                Email = "foo@bar.com",
            };
            return (dummyKlant);
        }
    }
}
