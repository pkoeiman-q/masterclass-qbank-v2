using MasterclassApiTest.Entities;
using MasterclassApiTest.Models;
using MasterclassApiTest.Repositories;

namespace MasterclassApiTest.Services
{
    public class RekeningenService : IRekeningenService
    {
        private readonly IRekeningenRepository _repository;
        public RekeningenService(IRekeningenRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RekeningDTO>> GetRekeningen(int klantId) => await _repository.GetRekeningen(klantId);

        public async Task<RekeningDTO?> GetSingleRekening(int klantId, string rekeningId) => await _repository.GetSingleRekening(klantId, rekeningId);

        public async Task<RekeningDTO?> CreateRekening(int klantId, RekeningDTO rekeningDTO) => await _repository.CreateRekening(klantId, rekeningDTO);

        public async Task<RekeningDTO?> UpdateRekening(RekeningDTO rekeningDTO) => await _repository.UpdateRekening(rekeningDTO);

        public async Task<RekeningDTO?> DeleteRekening(int klantId, string rekeningNummer) => await _repository.DeleteRekening(klantId, rekeningNummer);
    }
}
