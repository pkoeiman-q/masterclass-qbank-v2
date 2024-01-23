using MasterclassApiTest.Models;

namespace MasterclassApiTest.Repositories
{
    public interface IRekeningenRepository
    {
        Task<RekeningDTO?> CreateRekening(int klantId, RekeningDTO dto);
        Task<RekeningDTO?> DeleteRekening(int klantId, string rekeningId);
        Task<List<RekeningDTO>> GetRekeningen(int klantId);
        Task<RekeningDTO?> GetSingleRekening(int klantId, string rekeningId);
        Task<RekeningDTO?> UpdateRekening(RekeningDTO dto);
    }
}