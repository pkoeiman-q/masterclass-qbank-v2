using System.ComponentModel.DataAnnotations;

namespace MasterclassApiTest.Entities
{
    public class Adres
    {
        [Required]
        public string Straat { get; set; }
        [Required]
        public int Huisnummer { get; set; }
        public string? HuisnummerToevoeging { get; set; }
        [Required]
        public string Postcode { get; set; }
        [Required]
        public string Woonplaats { get; set; }
    }
}
