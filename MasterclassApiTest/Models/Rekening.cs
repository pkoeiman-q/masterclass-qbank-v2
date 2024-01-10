namespace MasterclassApiTest.Models
{
    public class Rekening
    {
        public string RekeningNummer { get; set; }
        public int KlantNummer { get; set; }
        public string Type { get; set; }
        public string Saldo { get; set; }
        public string Status { get; set; }
        public DateTime BeginDatum { get; set; }
    }
}
