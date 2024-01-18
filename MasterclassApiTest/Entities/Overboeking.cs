using System.ComponentModel.DataAnnotations;

namespace MasterclassApiTest.Entities
{
    public class Overboeking
    {
        [Key]
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
