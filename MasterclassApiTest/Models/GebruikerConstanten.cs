using MasterclassApiTest.Entities;

namespace MasterclassApiTest.Models
{
    public class GebruikerConstanten
    {
        public static List<Gebruiker> Gebruikers = new List<Gebruiker>()
        {
            new Gebruiker()
            {
                LoginNaam = "adam",
                Email = "foo1@bar.com",
                Wachtwoord = "a",
                Role = UserAccessRole.Medewerker,
                DisplayNaam = "Adam",
                LaatstIngelogd = DateTime.UnixEpoch
    },
            new Gebruiker()
            {
                LoginNaam = "eve",
                Email = "foo2@bar.com",
                Wachtwoord = "e",
                Role = UserAccessRole.Klant,
                DisplayNaam = "Eve",
                LaatstIngelogd = DateTime.UnixEpoch
            },
        };
    }
}
