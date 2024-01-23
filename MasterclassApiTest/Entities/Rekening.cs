using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterclassApiTest.Entities
{
    public class Rekening
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string RekeningNummer { get; set; }
        [Required]
        public Klant Klant { get; set; }
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
