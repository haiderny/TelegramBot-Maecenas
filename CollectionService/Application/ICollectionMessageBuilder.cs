using CollectionService.Domain;

namespace CollectionService.Application
{
    public interface ICollectionMessageBuilder
    {
        void AddTarget(string target);
        void AddAmount(int amount);
        void AddTime(string time);
        Collection Build();
    }
}