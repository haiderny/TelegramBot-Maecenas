using System.Threading.Tasks;
using MongoDB.Driver;
using UserService.Entities;
using UserService.Infrastructure;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<User> GetUserById(int id)
        {
            var database = _session.GetDatabase($"{Properties.Resources.nameOfDatabase}");
            var collection = database.GetCollection<User>($"{Properties.Resources.nameOfCollectionUsers}");
            var filter = Builders<User>.Filter.Eq("Id", id);
            var users = await collection.Find(filter).ToListAsync();
            User user = null;
            foreach (var findUser in users)
            {
                user = findUser;
            }
            return user;
        }

        public async Task UpdateUser(User user)
        {
            var database = _session.GetDatabase($"{Properties.Resources.nameOfDatabase}");
            var collection = database.GetCollection<User>($"{Properties.Resources.nameOfCollectionUsers}");
            var filter = Builders<User>.Filter.Eq("Id", user.Id);
            await collection.ReplaceOneAsync(filter, user);
        }

        public Task CreateUser(User user)
        {
            var database = _session.GetDatabase($"{Properties.Resources.nameOfDatabase}");
            var collection = database.GetCollection<User>($"{Properties.Resources.nameOfCollectionUsers}");
            return collection.InsertOneAsync(user);
        }

        private readonly MongoClient _session;

        public UserRepository(MongoClient session)
        {
            _session = session;
        }
    }
}
