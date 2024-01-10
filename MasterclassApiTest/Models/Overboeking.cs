namespace MasterclassApiTest.Models
{
    public class Overboeking
    {
        public int volgNummer { get; set; }
        public DateTime boekDatum { get; set; }
        public int bedrag { get; set; }
        public Rekening vanRekening { get; set; }
        public Rekening tegenRekening { get; set; }
    }
}
