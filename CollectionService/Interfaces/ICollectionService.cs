using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace CollectionService.Interfaces
{
    public interface ICollectionService
    {
        Task<Collection> GetCollectionById(string collectionId, int userId);
        Task<IEnumerable<Collection>> GetCurrentCollectionsByUserId(int userId);
        Task<IEnumerable<Collection>> GetAllCollectionsByUserId(int userId);
        Task UpdateCollection(Collection collection, int userId);
    }
}