using MasterclassApiTest.Data;

namespace MasterclassApiTest.Repositories
{
    public class GebruikersRepository
    {
        private readonly DataContext _context;

        public GebruikersRepository(DataContext context)
        {
            _context = context;
        }
    }
}
