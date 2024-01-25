using MasterclassApiTest.Models;
using MasterclassApiTest.Pagination;

namespace MasterclassApiTest.Services
{
    public interface IKlantenService
    {
        Task<GetKlantDTO?> CreateKlant(CreateKlantDTO input);
        Task<GetKlantDTO?> DeleteKlant(int id);
        Task<List<GetKlantDTO>> GetAllKlanten(KlantPageParameters klantPageParameters);
        Task<GetKlantDTO?> GetKlant(int id);
        Task<GetKlantDTO?> UpdateKlant(int id, CreateKlantDTO input);
        Task<GetKlantDTO?> UpdateKlant(int id, GetKlantDTO input);
    }
}