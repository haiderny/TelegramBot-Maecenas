using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionService.Domain;
using Microsoft.Win32;
using MongoDB.Bson;

namespace CollectionService.Application
{
    public interface ICollectionService
    {
        Task CreateCollection(Collection collection);
        Task<Collection> GetCollectionById(ObjectId collectionId);
        Task<List<Collection>> GetCurrentCollectionsByUser(int userId);
        Task<List<Collection>> GetCompletedCollectionsByUser(int userId);
        Task<List<Collection>> GetAllCollectionsByUser(int userId);
        Task UpdateCollection(Collection collection);

    }
}