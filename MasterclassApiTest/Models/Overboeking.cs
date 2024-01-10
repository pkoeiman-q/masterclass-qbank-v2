namespace MasterclassApiTest.Models
{
    public class Overboeking
    {
        public int VolgNummer { get; set; }
        public DateTime BoekDatum { get; set; }
        public int Bedrag { get; set; }
        public Rekening VanRekening { get; set; }
        public Rekening TegenRekening { get; set; }
    }
}
