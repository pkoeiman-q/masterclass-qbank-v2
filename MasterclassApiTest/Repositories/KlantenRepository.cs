using MasterclassApiTest.Data;
using MasterclassApiTest.Entities;
using MasterclassApiTest.Models;
using MasterclassApiTest.Pagination;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace MasterclassApiTest.Repositories
{
    public class KlantenRepository
    {
        private readonly DataContext _context;

        public KlantenRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Klant>> GetAllKlanten(KlantPageParameters klantPageParameters)
        {
            var klanten = PagedList<Klant>
                .ToPagedList(
                    _context.Klanten.OrderBy(k => k.Id),
                    klantPageParameters.PageNumber,
                    klantPageParameters.PageSize
                );
                

            return klanten;
        }

        public async Task<Klant?> GetKlant(int id)
        {
            var klant = await _context.Klanten.FindAsync(id);
            return klant;
        }

        private Klant CreateKlantFromInput(KlantInput input)
        {
            Adres adres = new Adres
            {
                Straat = input.Straat,
                Huisnummer = input.Huisnummer,
                HuisnummerToevoeging = input.HuisnummerToevoeging,
                Postcode = input.Postcode,
                Woonplaats = input.Woonplaats
            };

            Klant klantToSave = new Klant
            {
                LoginNaam = input.LoginNaam,
                LaatstIngelogd = DateTime.Now,
                DisplayNaam = input.LoginNaam,
                Voorletters = input.Voorletters,
                Achternaam = input.Achternaam,
                Geslacht = input.Geslacht,
                GeboorteDatum = input.GeboorteDatum,
                OverlijdensDatum = input.OverlijdensDatum,
                Adres = adres,
                Bsn = input.Bsn,
                TelefoonNummer = input.TelefoonNummer,
                Email = input.Email,
            };
            return klantToSave;
        }

        public async Task<Klant> CreateKlant(KlantInput input)
        {
            Klant klantToSave = CreateKlantFromInput(input);
            _context.Klanten.Add(klantToSave);
            await _context.SaveChangesAsync();

            return klantToSave;
        }

        public async Task<Klant?> UpdateKlant (int id, KlantInput input)
        {
            Klant? klantToUpdate = await _context.Klanten.FindAsync(id);
            if (klantToUpdate == null)
            {
                return null;
            }

            Adres adres = new Adres
            {
                Straat = input.Straat,
                Huisnummer = input.Huisnummer,
                HuisnummerToevoeging = input.HuisnummerToevoeging,
                Postcode = input.Postcode,
                Woonplaats = input.Woonplaats
            };

            klantToUpdate.LoginNaam = input.LoginNaam;
            klantToUpdate.DisplayNaam = input.LoginNaam;
            klantToUpdate.Voorletters = input.Voorletters;
            klantToUpdate.Achternaam = input.Achternaam;
            klantToUpdate.Geslacht = input.Geslacht;
            klantToUpdate.GeboorteDatum = input.GeboorteDatum;
            klantToUpdate.OverlijdensDatum = input.OverlijdensDatum;
            klantToUpdate.Adres = adres;
            klantToUpdate.Bsn = input.Bsn;
            klantToUpdate.TelefoonNummer = input.TelefoonNummer;
            klantToUpdate.Email = input.Email;

            await _context.SaveChangesAsync();
            return klantToUpdate;
        }

        public async Task<Klant?> DeleteKlant(int id)
        {
            Klant? klantToDelete = await _context.Klanten.FindAsync(id);
            if (klantToDelete == null)
            {
                return null;
            }

            _context.Remove(klantToDelete);
            await _context.SaveChangesAsync();
            return klantToDelete;
        }
    }
}
