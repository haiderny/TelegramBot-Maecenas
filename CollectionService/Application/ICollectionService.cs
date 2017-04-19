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
        Task<List<Collection>> GetCurrentCollectionsByUserId(int userId);
        Task<List<Collection>> GetCompletedCollectionsByUserId(int userId);
        Task<List<Collection>> GetAllCollectionsByUserId(int userId);
        Task UpdateCollection(Collection collection);

    }
}