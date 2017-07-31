using DataAccess.Entities;

namespace DataAccess.Application
{
    public interface ICollectionMessageBuilder
    {
        void AddTarget(string target);
        void AddAmount(int amount);
        void AddTime(string time);
        void AddNumberCard(ulong number);
        Collection Build();
    }
}