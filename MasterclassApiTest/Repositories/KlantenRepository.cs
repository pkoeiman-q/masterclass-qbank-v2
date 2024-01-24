using MasterclassApiTest.Data;
using MasterclassApiTest.Entities;
using MasterclassApiTest.Models;
using MasterclassApiTest.Pagination;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Numerics;

namespace MasterclassApiTest.Repositories
{
    public class KlantenRepository : IKlantenRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public KlantenRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedList<GetKlantDTO>> GetAllKlanten(KlantPageParameters klantPageParameters)
        {
            var query = GetKlantenAsDTO();

            var list = PagedList<GetKlantDTO>
                .ToPagedList(
                    //query.OrderBy(k => k.GetType().GetProperty(klantPageParameters.AttributeToSortBy)),
                    query.OrderBy(k => k.Achternaam),
                    klantPageParameters.PageNumber,
                    klantPageParameters.PageSize
                );

            return list;
        }

        private IQueryable<GetKlantDTO> GetKlantenAsDTO()
        {
            var query = from klant in _context.Klanten
                        select new GetKlantDTO
                        {
                            Id = klant.Id,
                            Achternaam = klant.Achternaam,
                            Voorletters = klant.Voorletters,
                            Email = klant.Email,
                            GeboorteDatum = klant.GeboorteDatum,
                            Geslacht = klant.Geslacht,
                            Adres = klant.Adres,
                            OverlijdensDatum = klant.OverlijdensDatum,
                            TelefoonNummer = klant.TelefoonNummer,
                        };
            return query;
        }

        private GetKlantDTO ConvertSingleToGetKlantDTO(Klant klant)
        {
            GetKlantDTO klantDTO = _mapper.Map<GetKlantDTO>(klant);
            return klantDTO;
        }

        public async Task<GetKlantDTO?> GetKlant(int id)
        {
            var query = GetKlantenAsDTO();
            var klant = await query.SingleOrDefaultAsync(k => k.Id == id);
            return klant;
        }

        private Klant CreateKlantFromInput(CreateKlantDTO input)
        {
            Klant klantToSave = _mapper.Map<Klant>(input);
            return klantToSave;
        }

        public async Task<GetKlantDTO> CreateKlant(CreateKlantDTO input)
        {
            Klant klantToSave = CreateKlantFromInput(input);
            _context.Klanten.Add(klantToSave);
            await _context.SaveChangesAsync();

            return ConvertSingleToGetKlantDTO(klantToSave);
        }

        public async Task<GetKlantDTO?> UpdateKlant(int id, CreateKlantDTO input)
        {
            Klant? klantToUpdate = await _context.Klanten.FindAsync(id);
            if (klantToUpdate == null)
            {
                return null;
            }

            // AutoMapper doesn't work with the change tracker here
            klantToUpdate.LoginNaam = input.LoginNaam;
            klantToUpdate.DisplayNaam = input.LoginNaam;
            klantToUpdate.Voorletters = input.Voorletters;
            klantToUpdate.Achternaam = input.Achternaam;
            klantToUpdate.Geslacht = input.Geslacht;
            klantToUpdate.GeboorteDatum = input.GeboorteDatum;
            klantToUpdate.OverlijdensDatum = input.OverlijdensDatum;
            klantToUpdate.Adres = input.Adres;
            klantToUpdate.Bsn = input.Bsn;
            klantToUpdate.TelefoonNummer = input.TelefoonNummer;
            klantToUpdate.Email = input.Email;

            await _context.SaveChangesAsync();
            return ConvertSingleToGetKlantDTO(klantToUpdate);
        }

        public async Task<GetKlantDTO?> DeleteKlant(int id)
        {
            Klant? klantToDelete = await _context.Klanten.FindAsync(id);
            if (klantToDelete == null)
            {
                return null;
            }

            _context.Remove(klantToDelete);
            await _context.SaveChangesAsync();
            return ConvertSingleToGetKlantDTO(klantToDelete);
        }
    }
}
