using System.ComponentModel.DataAnnotations;

namespace MasterclassApiTest.Models
{
    public class Klant
    {
        [Required]
        public int KlantNummer { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string LoginNaam { get; set; }
        [Required]
        public DateTime LaatstIngelogd { get; set; }
        [Required]
        public string DisplayNaam { get; set; }
        [Required]
        public string Voorletters { get; set; }
        [Required]
        public string Achternaam { get; set; }
        [Required]
        public string Geslacht { get; set; }
        [Required]
        public DateTime GeboorteDatum { get; set; }
        public DateTime? OverlijdensDatum { get; set; }
        [Required]
        public string Straat { get; set; }
        [Required]
        public int Huisnummer { get; set; }
        public string? HuisnummerToevoeging { get; set; }
        [Required]
        public string Postcode { get; set; }
        [Required]
        public string Woonplaats { get; set; }
        [Required]
        public int Bsn { get; set; }
        [Required]
        public string TelefoonNummer { get; set; }
        [Required]
        public string Email { get; set; }

        static public List<Klant> ZoekKlant(List<Klant> klantList, string searchType, string searchTerm)
        {
            // List of customers to search through, the attribute to look at (search type) and the term to search for
            List<Klant> klanten = new List<Klant>();
            searchType = searchType.ToLower();
            searchTerm = searchTerm.ToLower();
            string matchTerm = "";

            // Return an empty list if no search term was provided
            if (string.IsNullOrWhiteSpace(searchTerm)) {
                return klanten;
            }

            string[] searchTypes = [searchType];
            if (searchType == "all")
            {
                searchTypes = ["displaynaam", "achternaam", "straat", "woonplaats", "email"];
            }

            foreach (Klant klant in klantList)
            {
                // Prevent the same customer from being added to the search results multiple times
                bool klantIncluded = false;
                foreach (string type in searchTypes)
                {
                    switch (type)
                    {
                        case "displaynaam":
                            matchTerm = klant.DisplayNaam;
                            break;
                        case "achternaam":
                            matchTerm = klant.Achternaam;
                            break;
                        case "straat":
                            matchTerm = klant.Straat;
                            break;
                        case "woonplaats":
                            matchTerm = klant.Woonplaats;
                            break;
                        case "email":
                            matchTerm = klant.Email;
                            break;
                        // If the search terms in the array didn't match any of the above cases, exit prematurely
                        // We won't find customers
                        default:
                            return klanten;
                    }
                }

                if (!klantIncluded && matchTerm.ToLower().Contains(searchTerm))
                {
                    klantIncluded = true;
                    klanten.Add(klant);
                }
            }

            return klanten;
        }
    }
}
