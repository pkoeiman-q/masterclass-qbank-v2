using AutoMapper;
using FakeItEasy;
using MasterclassApiTest.Builders;
using MasterclassApiTest.Controllers.v2;
using MasterclassApiTest.Data;
using MasterclassApiTest.Entities;
using MasterclassApiTest.Models;
using MasterclassApiTest.Pagination;
using MasterclassApiTest.Services;

namespace MasterclassApiTest.Fixtures
{
    public class KlantenControllerFixture
    {
        public KlantenController Sut { get; }
        public KlantPageParameters PageParams;

        private List<GetKlantDTO> KlantList()
        {
            var klanten = new List<GetKlantDTO>();
            var builder = new KlantEntityBuilder();

            GetKlantDTO klantA = builder.WithLoginNaam("Klant A").BuildGetKlantDTO();
            GetKlantDTO klantB = builder.WithLoginNaam("Klant B").BuildGetKlantDTO();
            klanten.Add(klantA);
            klanten.Add(klantB);

            return klanten;
        }

        public KlantenControllerFixture()
        {
            // Dependencies of the KlantenController
            var unitOfWork = A.Fake<IUnitOfWork>();

            PageParams = new KlantPageParameters
            {
                PageNumber = 1,
                PageSize = 10,
            };

            var klanten = KlantList();
            A.CallTo(() => unitOfWork.Klanten.GetAllKlanten(PageParams))
                .Returns(klanten);
            Sut = new KlantenController(unitOfWork);
        }
    }
}
