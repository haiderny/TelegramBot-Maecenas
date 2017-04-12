using System.Threading.Tasks;
using CollectionService.Domain;

namespace CollectionService.Application
{
    public interface ICollectionService
    {
        Task CreateCollection(int authorId, string name);
        Collection GetCollectionById(int collectionId);
        CollectionMessageBuilder GetBuilderForCollection(int autorId);
    }
}