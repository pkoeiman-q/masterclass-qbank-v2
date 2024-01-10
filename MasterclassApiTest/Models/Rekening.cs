namespace MasterclassApiTest.Models
{
    public class Rekening
    {
        public string rekeningNummer { get; set; }
        public int klantNummer { get; set; }
        public string type { get; set; }
        public string saldo { get; set; }
        public string status { get; set; }
        public DateTime beginDatum { get; set; }
    }
}
