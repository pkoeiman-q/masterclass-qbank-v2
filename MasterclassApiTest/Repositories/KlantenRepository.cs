using MasterclassApiTest.Data;
using MasterclassApiTest.Entities;
using Microsoft.EntityFrameworkCore;

namespace MasterclassApiTest.Repositories
{
    public class KlantenRepository
    {
        private readonly DataContext _context;

        public KlantenRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Klant>> GetAllKlanten()
        {
            var klanten = await _context.Klanten.ToListAsync();
            return klanten;
        }
    }
}
