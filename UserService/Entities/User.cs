using System.Collections.Generic;
using CollectionService.Domain;

namespace UserService.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Collection> Collections { get; set; }

        public int NumberYandexPurse { get; set; }

        public int NumberCreditCard { get; set; }

        public UserStatus UserStatus { get; set; }

        public CollectionMessageBuilder Builder { get; set; }

        public User(int id, string firstName, string lastName, List<Collection> collections, UserStatus userStatus, CollectionMessageBuilder builder)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Collections = collections;
            UserStatus = userStatus;
            Builder = builder;
        }
    }
}
