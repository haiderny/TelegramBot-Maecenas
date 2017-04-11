using CollectionService.Domain;

namespace CollectionService.Application
{
    public interface ICollectionMessageBuilder
    {
        void AddTarget(string target);
        void AddAmmount(decimal ammount);
        void AddTime(string time);
        Collection Build();
    }
}