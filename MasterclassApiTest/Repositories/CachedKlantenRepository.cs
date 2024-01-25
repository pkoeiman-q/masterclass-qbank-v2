using MasterclassApiTest.Data;
using MasterclassApiTest.Entities;
using MasterclassApiTest.Helpers;
using MasterclassApiTest.Models;
using MasterclassApiTest.Pagination;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace MasterclassApiTest.Repositories
{
    // Tutorial: https://www.youtube.com/watch?v=i_3I6XLAOt0&ab_channel=MilanJovanovi%C4%87
    // This is called the decorater pattern
    // This class calls the class that is being decorated and provides additional functionality ontop of it
    // In this case: check if the result is cached first, then make a trip to the DB if it wasn't cached yet (or if the result is different now)
    public class CachedKlantenRepository : IKlantenRepository
    {
        private readonly KlantenRepository _decorated;
        private readonly IDistributedCache _distributedCache;

        public CachedKlantenRepository(KlantenRepository decorated, IDistributedCache distributedCache)
        {
            _decorated = decorated;
            _distributedCache = distributedCache;
        }

        private string RedisKey(int id)
        {
            return $"klant-{id}";
        }

        private async Task<string?> GetKlantFromCacheById(int id)
        {
            string key = RedisKey(id);
            try
            {
                return await _distributedCache.GetStringAsync(key);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Tried to READ from Redis cache with {key}, but ran into an error:\n{ex.Message}");
                return null;
            }
        }

        private async void SetKlantInCache(GetKlantDTO klantObject)
        {
            string key = RedisKey(klantObject.Id);
            try
            {
                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(klantObject));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Tried to WRITE to Redis cache with {key}, but ran into an error:\n{ex.Message}");
            }
        }

        // CREATE
        public async Task<GetKlantDTO> CreateKlant(CreateKlantDTO input)
        {
            GetKlantDTO createdKlant = await _decorated.CreateKlant(input);

            // Update the cache to include/overwrite the klant there
            SetKlantInCache(createdKlant);

            return createdKlant;
        }

        // DELETE
        public async Task<GetKlantDTO?> DeleteKlant(int id)
        {
            string? cachedKlant = await GetKlantFromCacheById(id);

            // Remove the cached klant if found there
            if (!string.IsNullOrEmpty(cachedKlant))
            {
                await _distributedCache.RemoveAsync(RedisKey(id));
            }

            return await _decorated.DeleteKlant(id);
        }

        // GET ALL
        public Task<PagedList<GetKlantDTO>> GetAllKlanten(KlantPageParameters klantPageParameters)
        {
            return _decorated.GetAllKlanten(klantPageParameters);
        }

        // GET SINGLE
        public async Task<GetKlantDTO?> GetKlant(int id)
        {
            // Get a serialized version of the klant from the cache
            string key = RedisKey(id);
            string? cachedKlant = await GetKlantFromCacheById(id);

            // If the klant wasn't found in the cache...
            GetKlantDTO? klant;
            if (string.IsNullOrEmpty(cachedKlant))
            {
                // Get the requested klant
                // Return null if not found
                klant = await _decorated.GetKlant(id);
                if (klant != null)
                {
                    // Cache the Klant and return it
                    SetKlantInCache(klant);
                }
                return klant;
            }

            // If it was found, deserialize the cached klant and return it
            klant = JsonConvert.DeserializeObject<GetKlantDTO>(
                cachedKlant,
                new JsonSerializerSettings
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                    ContractResolver = new PrivateResolver()
                });

            return klant;
        }

        // UPDATE
        public async Task<GetKlantDTO?> UpdateKlant(int id, CreateKlantDTO input)
        {
            GetKlantDTO? updatedKlant = await _decorated.UpdateKlant(id, input);

            // Update the cache to include/overwrite the klant there
            if (updatedKlant != null) SetKlantInCache(updatedKlant);
            return updatedKlant;
        }

        public async Task<GetKlantDTO?> UpdateKlant(int id, GetKlantDTO input)
        {
            GetKlantDTO? updatedKlant = await _decorated.UpdateKlant(id, input);

            // Update the cache to include/overwrite the klant there
            if (updatedKlant != null) SetKlantInCache(updatedKlant);
            return updatedKlant;
        }
    }
}
