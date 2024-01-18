﻿using System.ComponentModel.DataAnnotations;

namespace MasterclassApiTest.Entities
{
    public class Klant
    {
        [Key]
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
        public Adres Adres { get; set; }
        [Required]
        public int Bsn { get; set; }
        [Required]
        public string TelefoonNummer { get; set; }
        [Required]
        public string Email { get; set; }
        public List<Rekening> Rekeningen { get; set; }
    }
}
