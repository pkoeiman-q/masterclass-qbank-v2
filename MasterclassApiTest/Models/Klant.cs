using System.ComponentModel.DataAnnotations;

namespace MasterclassApiTest.Models
{
    public class Klant
    {
        [Required]
        public int KlantNummer { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string LoginNaam { get; set; }
        [Required]
        public DateTime LaatstIngelogd { get; set; }
        [Required]
        public string DisplayNaam { get; set; }
        [Required]
        public string Voorletters { get; set; }
        [Required]
        public string Achternaam { get; set; }
        [Required]
        public string Geslacht { get; set; }
        [Required]
        public DateTime GeboorteDatum { get; set; }
        public DateTime? OverlijdensDatum { get; set; }
        [Required]
        public string Straat { get; set; }
        [Required]
        public int Huisnummer { get; set; }
        public string? HuisnummerToevoeging { get; set; }
        [Required]
        public string Postcode { get; set; }
        [Required]
        public string Woonplaats { get; set; }
        [Required]
        public int Bsn { get; set; }
        [Required]
        public string TelefoonNummer { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
