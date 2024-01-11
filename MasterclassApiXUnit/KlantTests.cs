using MasterclassApiTest.Controllers;
using MasterclassApiTest.Models;

namespace MasterclassApiXUnit
{
    public class KlantTests
    {
        [Theory]
        [InlineData("displaynaam", "display")]
        [InlineData("achternaam", "achter")]
        [InlineData("straat", "straat")]
        [InlineData("woonplaats", "woonplaats")]
        [InlineData("email", "foo@bar.com")]
        public void ZoekKlantSucces(string searchType, string searchTerm)
        {
            // Arrange
            List<Klant> klanten = GenereerKlantLijst();

            // Act
            List<Klant> gevondenKlanten = Klant.ZoekKlant(klanten, searchType, searchTerm);

            // Assert
            Assert.Single(gevondenKlanten);
        }

        [Fact]
        public void ZoekKlantFail()
        {
            // Arrange
            List<Klant> klanten = GenereerKlantLijst();

            // Act
            List<Klant> gevondenKlanten = Klant.ZoekKlant(klanten, "displaynaam", "Fake data");

            // Assert
            Assert.Empty(gevondenKlanten);
        }

        public List<Klant> GenereerKlantLijst()
        {
            List<Klant> klanten = new List<Klant>();
            Klant dummyKlantA = new Klant
            {
                KlantNummer = klanten.Count(),
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
            klanten.Add(dummyKlantA);
            return klanten;
        }
    }
}