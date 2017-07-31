using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Application
{
    public interface ICollectionRepository
    {
        Task<Collection> GetCollectionById(string id, int userId);
        Task<IEnumerable<Collection>> GetCurrentCollectionsByUserId(int userId);
        Task<IEnumerable<Collection>> GetAllCollectionsByUserId(int userId);
        Task UpdateCollection(Collection collection, int userId);
    }
}
