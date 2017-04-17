using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionService.Domain;
using MongoDB.Bson;

namespace CollectionService.Infrastructure
{
    public interface ICollectionRepository
    {
        Task SaveCollection(Collection collection);
        Task<Collection> GetCollectionById(ObjectId id);
        Task<List<Collection>> GetCurrrentCollectionsByUserId(int userId);
        Task<List<Collection>> GetCompletedCollectionsByUserId(int userId);
        Task<List<Collection>> GetAllCollectionsByUserId(int userId);
        Task UpdateCollection(Collection collection);
    }
}
