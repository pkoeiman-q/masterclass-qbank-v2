using MasterclassApiTest.Data;
using MasterclassApiTest.Entities;
using MasterclassApiTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Net.NetworkInformation;
using System.Numerics;

namespace MasterclassApiTest.Repositories
{
    public class RekeningenRepository : IRekeningenRepository
    {
        private readonly DataContext _context;

        public RekeningenRepository(DataContext context)
        {
            _context = context;
        }

        private RekeningDTO ConvertRekeningToDTO(Rekening rekening, Klant klant)
        {
            return new RekeningDTO
            {
                RekeningNummer = rekening.RekeningNummer,
                KlantNummer = klant.Id,
                NaamEigenaar = $"{klant.Voorletters} {klant.Achternaam}",
                Type = rekening.Type,
                Saldo = rekening.Saldo,
                Status = rekening.Status,
                BeginDatum = rekening.BeginDatum,
            };
        }

        private IQueryable<Rekening> QueryRekeningen(Klant klant)
        {
            var query = from rekening in _context.Rekeningen
                        where rekening.Klant.Id == klant.Id
                        select rekening;
            return query;
        }

        private IQueryable<Rekening> QuerySingleRekening(Klant klant, string rekeningId)
        {
            var query = from rekening in _context.Rekeningen
                        where rekening.Klant.Id == klant.Id
                        where rekening.RekeningNummer == rekeningId
                        select rekening;
            return query;
        }

        private async Task<Klant?> FindKlantById(int klantId)
        {
            return await _context.Klanten.Where(k => k.Id == klantId).FirstOrDefaultAsync();
        }

        // Read all for given customer (by id)
        public async Task<List<RekeningDTO>> GetRekeningen(int klantId)
        {
            Klant? klant = await FindKlantById(klantId);
            if (klant == null) return new List<RekeningDTO>();

            var query = QueryRekeningen(klant);
            var rekeningen = await query.ToListAsync();

            List<RekeningDTO> dtos = new List<RekeningDTO>();
            foreach (Rekening r in rekeningen)
            {
                dtos.Add(ConvertRekeningToDTO(r, klant));
            }
            return dtos;
        }

        // Read by id
        public async Task<RekeningDTO?> GetSingleRekening(int klantId, string rekeningId)
        {
            Klant? klant = await FindKlantById(klantId);
            if (klant == null) return null;

            var query = QuerySingleRekening(klant, rekeningId);
            var rekening = await query.SingleOrDefaultAsync();
            if (rekening == null) return null;

            return ConvertRekeningToDTO(rekening, klant);
        }

        // Create for given customer
        public async Task<RekeningDTO?> CreateRekening(int klantId, RekeningDTO dto)
        {
            Klant? klant = await FindKlantById(klantId);
            if (klant == null) return null;

            Rekening rekeningToAdd = new Rekening
            {
                Klant = klant,
                Saldo = dto.Saldo,
                BeginDatum = dto.BeginDatum,
                Status = dto.Status,
                Type = dto.Type,
            };

            _context.Rekeningen.Add(rekeningToAdd);
            // No SaveChanges, as this is handled by UnitOfWork
            return ConvertRekeningToDTO(rekeningToAdd, klant);
        }

        // Update for given customer
        public async Task<RekeningDTO?> UpdateRekening(RekeningDTO dto)
        {
            Klant? associatedKlant = await FindKlantById(dto.KlantNummer);
            Rekening? rekeningToUpdate = await _context.Rekeningen.FindAsync(dto.RekeningNummer);

            if (rekeningToUpdate == null || associatedKlant == null)
            {
                return null;
            }

            rekeningToUpdate.Klant = associatedKlant;
            rekeningToUpdate.Saldo = dto.Saldo;
            rekeningToUpdate.Status = dto.Status;
            rekeningToUpdate.Type = dto.Type;
            rekeningToUpdate.BeginDatum = dto.BeginDatum;

            _context.Rekeningen.Update(rekeningToUpdate);
            // No SaveChanges, as this is handled by UnitOfWork
            return ConvertRekeningToDTO(rekeningToUpdate, associatedKlant);
        }

        // Delete by id
        public async Task<RekeningDTO?> DeleteRekening(int klantId, string rekeningId)
        {
            Rekening? rekeningToDelete = await _context.Rekeningen.FindAsync(rekeningId);
            if (rekeningToDelete == null) return null;

            Klant? klant = await FindKlantById(klantId);
            if (klant == null) return null;

            _context.Rekeningen.Remove(rekeningToDelete);
            // No SaveChanges, as this is handled by UnitOfWork
            return ConvertRekeningToDTO(rekeningToDelete, klant);
        }
    }
}
