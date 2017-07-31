using System.Collections.Generic;
using DataAccess.Application;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IList<Collection> Collections { get; set; }

        public UserStatus UserStatus { get; set; }

        [BsonSerializer(typeof(ImpliedImplementationInterfaceSerializer<ICollectionMessageBuilder, CollectionMessageBuilder>))]
        public ICollectionMessageBuilder Builder { get; set; }

        public User(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Collections = new List<Collection>();
            UserStatus = UserStatus.New;
            Builder = new CollectionMessageBuilder();
        }
    }
}
