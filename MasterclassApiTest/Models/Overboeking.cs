using System.ComponentModel.DataAnnotations;

namespace MasterclassApiTest.Models
{
    public class Overboeking
    {
        [Required]
        public int VolgNummer { get; set; }
        [Required]
        public DateTime BoekDatum { get; set; }
        [Required]
        public int Bedrag { get; set; }
        [Required]
        public string VanRekening { get; set; }
        [Required]
        public string TegenRekening { get; set; }
    }
}
