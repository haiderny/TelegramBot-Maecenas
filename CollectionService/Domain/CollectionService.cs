using System.Threading.Tasks;
using CollectionService.Application;

namespace CollectionService.Domain
{
    public class CollectionService : ICollectionService
    {
        public Task CreateCollection(int authorId, string name)
        {
            throw new System.NotImplementedException();
        }

        public Collection GetCollectionById(int collectionId)
        {
            throw new System.NotImplementedException();
        }

        public CollectionMessageBuilder GetBuilderForCollection(int autorId)
        {
            throw new System.NotImplementedException();
        }
    }
}
