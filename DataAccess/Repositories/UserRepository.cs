using System.Threading.Tasks;
using Journalist;
using MongoDB.Driver;
using UserService.Entities;
using UserService.Infrastructure;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<User> GetUserById(int id)
        {
            Require.Positive(id, nameof(id));

            return await _userCollection.Find(user => user.Id == id).SingleOrDefaultAsync();
        }

        public async Task UpdateUser(User user)
        {

            var filter = Builders<User>.Filter.Eq("Id", user.Id);
            await _userCollection.ReplaceOneAsync(filter, user);
        }

        public Task CreateUser(User user)
        {
            Require.NotNull(user, nameof(user));

            return _userCollection.InsertOneAsync(user);
        }
        
        private readonly IMongoCollection<User> _userCollection;

        public UserRepository(IMongoCollection<User> userCollection)
        {
            _userCollection = userCollection;
        }
    }
}
