using MasterclassApiTest.Services;

namespace MasterclassApiTest.Data
{
    public interface IUnitOfWork : IDisposable
    {
        GebruikersService Gebruikers { get; }
        IKlantenService Klanten { get; }
        IRekeningenService Rekeningen { get; }

        Task<int> Complete();
        void Dispose();
    }
}