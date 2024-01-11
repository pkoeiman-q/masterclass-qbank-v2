using System.ComponentModel.DataAnnotations;

namespace MasterclassApiTest.Models
{
    public class Rekening
    {
        [Required]
        public string RekeningNummer { get; set; }
        [Required]
        public int KlantNummer { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Saldo { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime BeginDatum { get; set; }
    }
}
