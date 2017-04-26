using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionService.Domain;

namespace CollectionService.Infrastructure
{
    public interface ICollectionRepository
    {
        Task<Collection> GetCollectionById(string id, int userId);
        Task<IEnumerable<Collection>> GetCurrrentCollectionsByUserId(int userId);
        Task<IEnumerable<Collection>> GetAllCollectionsByUserId(int userId);
        Task UpdateCollection(Collection collection, int userId);
    }
}
