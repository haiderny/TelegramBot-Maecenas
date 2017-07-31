using DataAccess.Entities;

namespace CollectionService.Interfaces
{
    public interface ICollectionBuilder
    {
        void AddTarget(string target);
        void AddAmount(int amount);
        void AddTime(string time);
        void AddNumberCard(ulong number);
        Collection Build();
    }
}