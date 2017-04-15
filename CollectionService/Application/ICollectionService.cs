using System.Threading.Tasks;
using CollectionService.Domain;

namespace CollectionService.Application
{
    public interface ICollectionService
    {
        Task CreateCollection(int authorId, Collection collection);
        Collection GetCollectionById(int collectionId);
    }
}