using System.ComponentModel.DataAnnotations;

namespace MasterclassApiTest.Entities
{
    public enum UserAccessRole
    {
        Klant,
        Medewerker
    }

    public class Gebruiker
    {
        public int Id { get; set; }
        public string LoginNaam { get; set; }
        public string Wachtwoord { get; set; } = "wachtwoord";
        public string Email { get; set; }
        public DateTime LaatstIngelogd { get; set; }
        public UserAccessRole Role { get; set; } = UserAccessRole.Klant;
        public string DisplayNaam { get; set; }
    }
}
