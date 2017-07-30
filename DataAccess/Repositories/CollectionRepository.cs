using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollectionService.Domain;
using CollectionService.Infrastructure;
using Journalist;
using UserService.Infrastructure;

namespace DataAccess.Repositories
{
    public class CollectionRepository : ICollectionRepository
    {
        public async Task<IEnumerable<Collection>> GetCurrentCollectionsByUserId(int userId)
        {
            Require.Positive(userId, nameof(userId));
            var user = await _userRepository.GetUserById(userId);
            return user.Collections.Where(collection => collection.Status);
        }

        public async Task<IEnumerable<Collection>> GetAllCollectionsByUserId(int userId)
        {
            Require.Positive(userId, nameof(userId));
            var user = await _userRepository.GetUserById(userId);
            return user.Collections;
        }

        public async Task UpdateCollection(Collection collection, int userId)
        {
            Require.Positive(userId, nameof(userId));
            var user = await _userRepository.GetUserById(userId);
            user.Collections[user.Collections.FindIndex(oldCollection => oldCollection._id == collection._id)] =
                collection;
            await _userRepository.UpdateUser(user);
        }

        public async Task<Collection> GetCollectionById(string id, int userId)
        {
            Require.NotEmpty(id, nameof(id));
            Require.Positive(userId, nameof(userId));
            var user = await _userRepository.GetUserById(userId);
            return user.Collections.Single(oldCollection => oldCollection._id == id);
        }
        
        private readonly IUserRepository _userRepository;

        public CollectionRepository(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

       
    }
}
