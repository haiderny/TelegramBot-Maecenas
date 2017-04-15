using System;
using System.Threading.Tasks;
using CollectionService.Application;

namespace CollectionService.Domain
{
    public class CollectionService : ICollectionService
    {
        public Task CreateCollection(int authorId, Collection collection)
        {
            throw new NotImplementedException();
        }

        public Collection GetCollectionById(int collectionId)
        {
            throw new System.NotImplementedException();
        }
    }
}
