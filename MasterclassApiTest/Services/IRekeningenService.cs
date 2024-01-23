using MasterclassApiTest.Models;

namespace MasterclassApiTest.Services
{
    public interface IRekeningenService
    {
        Task<RekeningDTO?> CreateRekening(int klantId, RekeningDTO rekeningDTO);
        Task<RekeningDTO?> DeleteRekening(int klantId, string rekeningNummer);
        Task<List<RekeningDTO>> GetRekeningen(int klantId);
        Task<RekeningDTO?> GetSingleRekening(int klantId, string rekeningId);
        Task<RekeningDTO?> UpdateRekening(RekeningDTO rekeningDTO);
    }
}