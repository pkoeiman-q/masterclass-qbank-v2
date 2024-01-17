using System.ComponentModel.DataAnnotations;

namespace MasterclassApiTest.Entities
{
    public class Gebruiker
    {
        [Key]
        public int Id { get; set; }
        public string GebruikersNaam { get; set; }
        public string Wachtwoord { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime LaatstIngelogd { get; set; }
    }
}
