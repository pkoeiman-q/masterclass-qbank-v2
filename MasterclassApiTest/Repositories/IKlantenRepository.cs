using MasterclassApiTest.Models;
using MasterclassApiTest.Pagination;

namespace MasterclassApiTest.Repositories
{
    public interface IKlantenRepository
    {
        Task<GetKlantDTO> CreateKlant(CreateKlantDTO input);
        Task<GetKlantDTO?> DeleteKlant(int id);
        Task<PagedList<GetKlantDTO>> GetAllKlanten(KlantPageParameters klantPageParameters);
        Task<GetKlantDTO?> GetKlant(int id);
        Task<GetKlantDTO?> UpdateKlant(int id, CreateKlantDTO input);
    }
}