using System.ComponentModel.DataAnnotations;

namespace MasterclassApiTest.Entities
{
    public class Gebruiker
    {
        public int Id { get; set; }
        public string LoginNaam { get; set; }
        public string Wachtwoord { get; set; } = "wachtwoord";
        public string Email { get; set; }
        public DateTime LaatstIngelogd { get; set; }
        public string Role { get; set; } = "Customer";
        public string DisplayNaam { get; set; }
    }
}
