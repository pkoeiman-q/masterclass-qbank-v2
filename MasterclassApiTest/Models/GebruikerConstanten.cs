using MasterclassApiTest.Entities;

namespace MasterclassApiTest.Models
{
    public class GebruikerConstanten
    {
        public static List<Gebruiker> Gebruikers = new List<Gebruiker>()
        {
            new Gebruiker()
            {
                GebruikersNaam = "adam",
                Email = "foo1@bar.com",
                Wachtwoord = "a",
                Role = "Admin"
            },
            new Gebruiker()
            {
                GebruikersNaam = "eve",
                Email = "foo2@bar.com",
                Wachtwoord = "e",
                Role = "Customer"
            },
        };
    }
}
