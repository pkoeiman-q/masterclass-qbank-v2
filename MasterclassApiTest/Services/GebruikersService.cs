using MasterclassApiTest.Repositories;

namespace MasterclassApiTest.Services
{
    public class GebruikersService
    {
        private readonly GebruikersRepository _repository;

        public GebruikersService(GebruikersRepository repository)
        {
            _repository = repository;
        }
    }
}
