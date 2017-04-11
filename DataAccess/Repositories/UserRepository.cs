using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using UserService.Domain;
using UserService.Infrastructure;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {

        public async void CreateUser(User user)
        {
            await SaveDocs(user);
        }

        private async Task SaveDocs(User user)
        {
            var database = _session.GetDatabase("telegram");
            var collection = database.GetCollection<User>("clients");
            await collection.InsertOneAsync(user);
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

        private readonly MongoClient _session;

        public UserRepository(MongoClient session)
        {
            _session = session;
        }
    }
}
