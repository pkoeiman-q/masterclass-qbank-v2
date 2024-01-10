namespace MasterclassApiTest.Models
{
    public class Klant
    {
        public int klantNummer { get; set; }
        public string loginNaam { get; set; }
        public DateTime laatstIngelogd { get; set; }
        public string displayNaam { get; set; }
        public string voorletters { get; set; }
        public string achternaam { get; set; }
        public string geslacht { get; set; }
        public DateTime geboorteDatum { get; set; }
        public DateTime overlijdensDatum { get; set; }
        public string straat { get; set; }
        public int huisnummer { get; set; }
        public string huisnummerToevoeging { get; set; }
        public string postcode { get; set; }
        public string woonplaats { get; set; }
        public int bsn { get; set; }
        public string telefoonNummer { get; set; }
        public string email { get; set; }
    }
}
