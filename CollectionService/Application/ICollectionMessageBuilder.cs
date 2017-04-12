using CollectionService.Domain;

namespace CollectionService.Application
{
    public interface ICollectionMessageBuilder
    {
        void AddTarget(string target);
        void AddAmount(decimal ammount);
        void AddTime(string time);
        Collection Build();
    }
}