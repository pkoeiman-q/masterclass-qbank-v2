﻿using MasterclassApiTest.Data;
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

        public IQueryable<GetKlantDTO> GetKlantenAsDTO()
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

        public GetKlantDTO ConvertSingleToGetKlantDTO(Klant klant)
        {
            GetKlantDTO klantDTO = new GetKlantDTO
            {
                Achternaam = klant.Achternaam,
                Adres = klant.Adres,
                Email = klant.Email,
                GeboorteDatum = klant.GeboorteDatum,
                Geslacht = klant.Geslacht,
                Id = klant.Id,
                OverlijdensDatum = klant.OverlijdensDatum,
                TelefoonNummer = klant.TelefoonNummer,
                Voorletters = klant.Voorletters,
            };

            return klantDTO;
        }

        public async Task<GetKlantDTO?> GetKlant(int id)
        {
            var query = GetKlantenAsDTO();
            var klant = await query.SingleAsync(k => k.Id == id);
            return klant;
        }

        private Klant CreateKlantFromInput(CreateKlantDTO input)
        {
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
                Adres = input.Adres,
                Bsn = input.Bsn,
                TelefoonNummer = input.TelefoonNummer,
                Email = input.Email,
            };
            return klantToSave;
        }

        public async Task<GetKlantDTO> CreateKlant(CreateKlantDTO input)
        {
            Klant klantToSave = CreateKlantFromInput(input);
            _context.Klanten.Add(klantToSave);
            await _context.SaveChangesAsync();

            return ConvertSingleToGetKlantDTO(klantToSave);
        }

        public async Task<GetKlantDTO?> UpdateKlant (int id, CreateKlantDTO input)
        {
            Klant? klantToUpdate = await _context.Klanten.FindAsync(id);
            if (klantToUpdate == null)
            {
                return null;
            }

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
