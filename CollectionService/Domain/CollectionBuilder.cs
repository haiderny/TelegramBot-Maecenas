using CollectionService.Interfaces;
using DataAccess.Application;
using DataAccess.Entities;
using Journalist;

namespace CollectionService.Domain
{
    public class CollectionBuilder : ICollectionBuilder
    {
        public void AddTarget(string target)
        {
            Require.NotEmpty(target, nameof(target));
            _collectionMessageBuilder.AddTarget(target);
        }

        public void AddAmount(int amount)
        {
            Require.Positive(amount, nameof(amount));
            _collectionMessageBuilder.AddAmount(amount);
        }

        public void AddTime(string time)
        {
            Require.NotEmpty(time, nameof(time));
            _collectionMessageBuilder.AddTime(time);
        }

        public void AddNumberCard(ulong number)
        {
            _collectionMessageBuilder.AddNumberCard(number);
        }

        public Collection Build()
        {
            return _collectionMessageBuilder.Build();
        }

        private readonly ICollectionMessageBuilder _collectionMessageBuilder;

        public CollectionBuilder(ICollectionMessageBuilder collectionMessageBuilder)
        {
            _collectionMessageBuilder = collectionMessageBuilder;
        }
    }
}
