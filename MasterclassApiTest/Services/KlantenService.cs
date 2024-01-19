using MasterclassApiTest.Entities;
using MasterclassApiTest.Models;
using MasterclassApiTest.Pagination;
using MasterclassApiTest.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MasterclassApiTest.Services
{
    public class KlantenService
    {
        private readonly KlantenRepository _repository;
        public KlantenService(KlantenRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetKlantDTO>> GetAllKlanten(KlantPageParameters klantPageParameters) => await _repository.GetAllKlanten(klantPageParameters);

        public async Task<GetKlantDTO?> GetKlant(int id) => await _repository.GetKlant(id);

        public async Task<GetKlantDTO?> CreateKlant(CreateKlantDTO input) => await _repository.CreateKlant(input);

        public async Task<GetKlantDTO?> UpdateKlant(int id, CreateKlantDTO input) => await _repository.UpdateKlant(id, input);

        public async Task<GetKlantDTO?> DeleteKlant(int id) => await _repository.DeleteKlant(id);

        static public List<Klant> ZoekKlant(List<Klant> klantList, string searchType, string searchTerm)
        {
            // List of customers to search through, the attribute to look at (search type) and the term to search for
            List<Klant> klanten = new List<Klant>();
            searchType = searchType.ToLower();
            searchTerm = searchTerm.ToLower();
            string matchTerm = "";

            // Return an empty list if no search term was provided
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
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
                            matchTerm = klant.Adres.Straat;
                            break;
                        case "woonplaats":
                            matchTerm = klant.Adres.Woonplaats;
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
