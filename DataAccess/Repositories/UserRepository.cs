using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using UserService.Entities;
using UserService.Infrastructure;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {

        public Task CreateUser(User user)
        {
            return SaveDocs(user);
        }

        public async Task<User> GetUserById(int id)
        {
            var database = _session.GetDatabase("telegram");
            var collection = database.GetCollection<User>("clients");
            var filter = Builders<User>.Filter.Eq("Id", id);
            var users = await collection.Find(filter).ToListAsync();
            User user = null;
            foreach (var p in users)
            {
                user = p;
            }
            return user;
        }

        private Task SaveDocs(User user)
        {
            var database = _session.GetDatabase("telegram");
            var collection = database.GetCollection<User>("clients");
            return collection.InsertOneAsync(user);
        }

        private readonly MongoClient _session;

        public UserRepository(MongoClient session)
        {
            _session = session;
        }
    }
}
