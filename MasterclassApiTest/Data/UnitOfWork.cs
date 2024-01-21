using MasterclassApiTest.Services;

namespace MasterclassApiTest.Data
{
    public class UnitOfWork : IDisposable
    {
        private readonly DataContext _context;
        public KlantenService Klanten { get; }
        public GebruikersService Gebruikers { get; }
        public UnitOfWork(DataContext context, KlantenService klantenService, GebruikersService gebruikersService)
        {
            _context = context;
            Klanten = klantenService;
            Gebruikers = gebruikersService;
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
