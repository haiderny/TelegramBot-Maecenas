using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionService.Domain;

namespace CollectionService.Application
{
    public interface ICollectionService
    {
        Task CreateCollection(Collection collection);
        Task<Collection> GetCollectionById(string collectionId);
        Task<IEnumerable<Collection>> GetCurrentCollectionsByUserId(int userId);
        Task<IEnumerable<Collection>> GetCompletedCollectionsByUserId(int userId);
        Task<IEnumerable<Collection>> GetAllCollectionsByUserId(int userId);
        Task UpdateCollection(Collection collection);

    }
}