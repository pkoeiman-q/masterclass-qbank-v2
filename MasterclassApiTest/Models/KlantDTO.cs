using MasterclassApiTest.Entities;
using System.ComponentModel.DataAnnotations;

namespace MasterclassApiTest.Models
{
    public class CreateKlantDTO
    {
        // Gebruiker attributes
        public string LoginNaam { get; set; }
        public string Wachtwoord { get; set; }
        public string Email { get; set; }
        public DateTime LaatstIngelogd { get; set; }
        [StringLength (30, ErrorMessage = "{0} mag niet meer dan {1} of minder dan {2} karakters bevatten.", MinimumLength = 3)]
        public string DisplayNaam { get; set; }

        // Klant info
        public string Voorletters { get; set; }
        public string Achternaam { get; set; }
        public string Geslacht { get; set; }
        public DateTime GeboorteDatum { get; set; }
        public DateTime? OverlijdensDatum { get; set; }
        public Adres Adres { get; set; }
        public int Bsn { get; set; }
        public string TelefoonNummer { get; set; }
    }

    public class GetKlantDTO
    {
        public int Id { get; set; }
        public string Voorletters { get; set; }
        public string Achternaam { get; set; }
        public string Geslacht { get; set; }
        public DateTime GeboorteDatum { get; set; }
        public DateTime? OverlijdensDatum { get; set; }
        public Adres Adres { get; set; }
        public string TelefoonNummer { get; set; }
        public string Email { get; set; }
    }
}
