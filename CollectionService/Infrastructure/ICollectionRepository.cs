using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CollectionService.Domain;

namespace CollectionService.Infrastructure
{
    public interface ICollectionRepository
    {
        Task SaveCollection(Collection collection);
        Task<Collection> GetCollectionById(string id);
        Task<IEnumerable<Collection>> GetCurrrentCollectionsByUserId(int userId);
        Task<IEnumerable<Collection>> GetCompletedCollectionsByUserId(int userId);
        Task<IEnumerable<Collection>> GetAllCollectionsByUserId(int userId);
        Task UpdateCollection(Collection collection);

        Task<IEnumerable<Collection>> GetCollections(Expression<Func<Collection, bool>> predicate);
    }
}
