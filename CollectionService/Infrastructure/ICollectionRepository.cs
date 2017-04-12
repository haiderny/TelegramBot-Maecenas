using System.Collections.Generic;
using CollectionService.Domain;

namespace CollectionService.Infrastructure
{
    public interface ICollectionRepository
    {
        void SaveCollection(Collection collection);
        List<Collection> GetCurrrentCollectionsByUserId(int userId);
        List<Collection> GetCompletedCollectionsByUserId(int userId);
        List<Collection> GetAllCollectionsByUserId(int userId);
        void UpdateCollection(Collection collection);
    }
}
