using DataAccess.Application;

namespace DataAccess.Entities
{
    public class CollectionMessageBuilder : ICollectionMessageBuilder
    {
        public string Target { get; private set; }

        public int Amount { get; private set; }

        public string Time { get; private set; }

        public ulong NumberCreditCard { get; private set; }

        public void AddTarget(string target)
        {
            Target = target;
        }

        public void AddAmount(int amount)
        {
            Amount = amount;
        }

        public void AddTime(string time)
        {
            Time = time;
        }

        public void AddNumberCard(ulong number)
        {
            NumberCreditCard = number;
        }

        public Collection Build()
        {
            var collection = new Collection(true, Target, Amount, Time, NumberCreditCard);
            return collection;
        }
    }
}
