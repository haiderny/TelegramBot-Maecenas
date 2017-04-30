using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionService.Application;
using CollectionService.Infrastructure;
using Journalist;

namespace CollectionService.Domain
{
    public class CollectionService : ICollectionService
    {
        public async Task<Collection> GetCollectionById(string collectionId, int userId)
        {
            Require.NotEmpty(collectionId, nameof(collectionId));

            return await _collectionRepository.GetCollectionById(collectionId, userId);
        }

        public async Task<IEnumerable<Collection>> GetCurrentCollectionsByUserId(int userId)
        {
            Require.Positive(userId, nameof(userId));

            return await _collectionRepository.GetCurrentCollectionsByUserId(userId);
        }

        public async Task<IEnumerable<Collection>> GetAllCollectionsByUserId(int userId)
        {
            Require.Positive(userId, nameof(userId));

            return await _collectionRepository.GetAllCollectionsByUserId(userId);
        }

        public async Task UpdateCollection(Collection collection, int userId)
        {
            Require.NotNull(collection, nameof(collection));

            await _collectionRepository.UpdateCollection(collection, userId);
        }

        private readonly ICollectionRepository _collectionRepository;

        public CollectionService(ICollectionRepository collectionRepository)
        {
            _collectionRepository = collectionRepository;
        }
    }
}
