using System;
using System.Collections.Generic;
using CollectionService.Domain;

namespace UserService.Domain
{
    public class User
    {
        public int Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public List<Collection> Collections { get; private set; }

        public UserStatus UserStatus { get; private set; }

        public User(int id, string firstName, string lastName, List<Collection> collections, UserStatus userStatus)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Collections = collections;
            UserStatus = userStatus;
        }
    }
}
