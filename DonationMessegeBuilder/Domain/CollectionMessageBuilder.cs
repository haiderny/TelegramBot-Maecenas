﻿using System.Collections.Generic;
using CollectionService.Domain;
using ICollectionMessageBuilder = DonationMessegeBuilder.Application.ICollectionMessageBuilder;

namespace DonationMessegeBuilder.Domain
{
    public class CollectionMessageBuilder : ICollectionMessageBuilder
    {
        public string Target { get; private set; }

        public int Amount { get; private set; }

        public string Time { get; private set; }

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

        public Collection Build()
        {
            var collection = new Collection(true, Target, Amount, Time, new List<string>());
            return collection;
        }
    }
}
