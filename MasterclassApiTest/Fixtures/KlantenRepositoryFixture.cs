using AutoMapper;
using MasterclassApiTest.AutoMapperProfiles;
using MasterclassApiTest.Builders;
using MasterclassApiTest.Data;
using MasterclassApiTest.Entities;
using MasterclassApiTest.Repositories;

namespace MasterclassApiTest.Fixtures
{
    // https://academy.quintor.nl/moodle/mod/lesson/view.php?id=1520&pageid=1339&startlastseen=no
    // Timestamp 5:45
    public class KlantenRepositoryFixture
    {
        private readonly DataContext _context;
        public DataContext Context => _context;

        public KlantenRepository Sut { get; }

        public KlantenRepositoryFixture()
        {
            _context = new DataContextBuilder()
                .UseSqlite()
                .Build();

            // About injecting automapper for tests: https://kenbonny.net/injecting-automapper-profiles-in-tests 
            var profile = new AutoMappingProfiles();
            var configuration = new MapperConfiguration(config => config.AddProfile(profile));
            var mapper = new Mapper(configuration);

            Sut = new KlantenRepository(_context, mapper);
        }

        public KlantenRepositoryFixture WithKlant(Klant klant)
        {
            _context.Klanten.Add(klant);
            _context.SaveChanges();
            return this;
        }

        public KlantenRepositoryFixture WithKlanten(List<Klant> klanten)
        {
            foreach (var klant in klanten)
            {
                _context.Klanten.Add(klant);
            }
            _context.SaveChanges();
            return this;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
