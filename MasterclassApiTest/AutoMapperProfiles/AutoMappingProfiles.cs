using MasterclassApiTest.Entities;
using MasterclassApiTest.Models;
using AutoMapper;

namespace MasterclassApiTest.AutoMapperProfiles
{
    public class AutoMappingProfiles : Profile
    {
        public AutoMappingProfiles() {
            CreateMap<Klant, GetKlantDTO>();
            CreateMap<CreateKlantDTO, Klant>();
        }
    }
}
