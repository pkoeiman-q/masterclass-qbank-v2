using System.ComponentModel.DataAnnotations;

namespace MasterclassApiTest.Models
{
    public class Klant
    {
        [Required]
        public int KlantNummer { get; set; }
        [StringLength(3)]
        public string LoginNaam { get; set; }
        public DateTime LaatstIngelogd { get; set; }
        public string DisplayNaam { get; set; }
        public string Voorletters { get; set; }
        public string Achternaam { get; set; }
        public string Geslacht { get; set; }
        public DateTime GeboorteDatum { get; set; }
        public DateTime OverlijdensDatum { get; set; }
        public string Straat { get; set; }
        public int Huisnummer { get; set; }
        public string HuisnummerToevoeging { get; set; }
        public string Postcode { get; set; }
        public string Woonplaats { get; set; }
        public int Bsn { get; set; }
        public string TelefoonNummer { get; set; }
        public string Email { get; set; }
    }
}
