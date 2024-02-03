using MasterclassApiTest.Entities;
using MasterclassApiTest.Models;

namespace MasterclassApiTest.Builders
{
    public class KlantEntityBuilder
    {
        private int _klantNummer = 0;
        private string _loginNaam = "dummy";

        public KlantEntityBuilder WithKlantNummer(int klantNummer)
        {
            _klantNummer = klantNummer;
            
            // By returning itself, we can chain other builder methods
            return this;
        }

        public KlantEntityBuilder WithLoginNaam(string loginNaam)
        {
            _loginNaam = loginNaam;
            return this;
        }

        public Klant Build()
        {
            Klant klant = new Klant
            {
                Id = _klantNummer,
                LoginNaam = _loginNaam,
                DisplayNaam = "Test Gebruiker",
                Voorletters = "BFG",
                Achternaam = "Buildette",
                Bsn = 111222333,
                Email = "foo@bar.com",
                GeboorteDatum = DateTime.Now,
                Geslacht = "Man",
                OverlijdensDatum = DateTime.Now,
                LaatstIngelogd = DateTime.UnixEpoch,
                Role = UserAccessRole.Klant,
                Adres = new Adres
                {
                    Huisnummer = 1,
                    Postcode = "1234AB",
                    Straat = "Zeestraat",
                    Woonplaats = "Den Haag",
                },
                TelefoonNummer = "06-12345678",
                Wachtwoord = "Open sesame",
        };
            return klant;
        }

        public GetKlantDTO BuildGetKlantDTO()
        {
            Klant klant = Build();
            GetKlantDTO dto = new GetKlantDTO()
            {
                Id = klant.Id,
                Voorletters = klant.Voorletters,
                Achternaam = klant.Achternaam,
                Email = klant.Email,
                GeboorteDatum = klant.GeboorteDatum,
                Geslacht = klant.Geslacht,
                OverlijdensDatum = klant.OverlijdensDatum,
                Adres = klant.Adres,
                TelefoonNummer = klant.TelefoonNummer,
            };
            return dto;
        }

        public CreateKlantDTO BuildCreateKlantDTO()
        {
            Klant klant = Build();
            CreateKlantDTO dto = new CreateKlantDTO()
            {
                Achternaam = klant.Achternaam,
                Email = klant.Email,
                Adres = klant.Adres,
                GeboorteDatum = klant.GeboorteDatum,
                Geslacht = klant.Geslacht,
                OverlijdensDatum = klant.OverlijdensDatum,
                TelefoonNummer = klant.TelefoonNummer,
                Voorletters = klant.Voorletters,
                Bsn = klant.Bsn,
                DisplayNaam = klant.DisplayNaam,
                LoginNaam = klant.LoginNaam,
                Wachtwoord = klant.Wachtwoord,
            };
            return dto;
        }
    }
}
