using System.ComponentModel.DataAnnotations;

namespace MasterclassApiTest.Entities
{
    public class Klant : Gebruiker
    {
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
        public Adres Adres { get; set; }
        [Required]
        public int Bsn { get; set; }
        [Required]
        public string TelefoonNummer { get; set; }
        public List<Rekening> Rekeningen { get; set; } = new List<Rekening>();
    }
}
