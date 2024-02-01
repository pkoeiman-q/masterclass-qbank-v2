using MasterclassApiTest.Services;

namespace MasterclassApiTest.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public IKlantenService Klanten { get; }
        public GebruikersService Gebruikers { get; }
        public IRekeningenService Rekeningen { get; }

        public UnitOfWork(DataContext context, IKlantenService klantenService, GebruikersService gebruikersService, IRekeningenService rekeningenService)
        {
            _context = context;
            Klanten = klantenService;
            Gebruikers = gebruikersService;
            Rekeningen = rekeningenService;
        }

        public async Task<int> Complete()
        {
            var count = await _context.SaveChangesAsync();
            return count;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
