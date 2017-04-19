using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionService.Application;
using CollectionService.Infrastructure;
using MongoDB.Bson;

namespace CollectionService.Domain
{
    public class CollectionService : ICollectionService
    {
        public async Task CreateCollection(Collection collection)
        {
            await _collectionRepository.SaveCollection(collection);
        }

        public async Task<Collection> GetCollectionById(string collectionId)
        {
            return await _collectionRepository.GetCollectionById(collectionId);
        }

        public async Task<List<Collection>> GetCurrentCollectionsByUserId(int userId)
        {
            return await _collectionRepository.GetCurrrentCollectionsByUserId(userId);
        }

        public async Task<List<Collection>> GetCompletedCollectionsByUserId(int userId)
        {
            return await _collectionRepository.GetCompletedCollectionsByUserId(userId);
        }

        public async Task<List<Collection>> GetAllCollectionsByUserId(int userId)
        {
            return await _collectionRepository.GetAllCollectionsByUserId(userId);
        }

        public async Task UpdateCollection(Collection collection)
        {
            await _collectionRepository.UpdateCollection(collection);
        }

        private readonly ICollectionRepository _collectionRepository;

        public CollectionService(ICollectionRepository collectionRepository)
        {
            _collectionRepository = collectionRepository;
        }
    }
}
