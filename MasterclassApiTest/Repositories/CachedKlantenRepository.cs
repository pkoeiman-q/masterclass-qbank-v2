using MasterclassApiTest.Models;
using MasterclassApiTest.Pagination;
using Microsoft.Extensions.Caching.Memory;

namespace MasterclassApiTest.Repositories
{
    // Tutorial: https://www.youtube.com/watch?v=i_3I6XLAOt0&ab_channel=MilanJovanovi%C4%87
    // This is called the decorater pattern
    // This class calls the class that is being decorated and provides additional functionality ontop of it
    // In this case: check if the result is cached first, then make a trip to the DB if it wasn't cached yet (or if the result is different now)
    public class CachedKlantenRepository : IKlantenRepository
    {
        private readonly KlantenRepository _decorated;
        private readonly IMemoryCache _memoryCache;

        public CachedKlantenRepository(KlantenRepository decorated, IMemoryCache memoryCache)
        {
            _decorated = decorated;
            _memoryCache = memoryCache;
        }

        public Task<GetKlantDTO> CreateKlant(CreateKlantDTO input)
        {
            return _decorated.CreateKlant(input);
        }

        public Task<GetKlantDTO?> DeleteKlant(int id)
        {
            return _decorated.DeleteKlant(id);
        }

        public Task<PagedList<GetKlantDTO>> GetAllKlanten(KlantPageParameters klantPageParameters)
        {
            return _decorated.GetAllKlanten(klantPageParameters);
        }

        public Task<GetKlantDTO?> GetKlant(int id)
        {
            string key = $"klant-{id}";

            // Check the cache if the value with the given cache key exists (if yes, return it)
            // Otherwise, perform the factory function to create and cache the value
            return _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                    return _decorated.GetKlant(id);
                });
        }

        public Task<GetKlantDTO?> UpdateKlant(int id, CreateKlantDTO input)
        {
            return _decorated.UpdateKlant(id, input);
        }
    }
}
