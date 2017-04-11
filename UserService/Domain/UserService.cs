using System.Threading.Tasks;
using Journalist;
using UserService.Application;
using UserService.Entities;
using UserService.Infrastructure;

namespace UserService.Domain
{
    public class UserService : IUserService
    {
        public Task<User> GetUserById(int userId)
        {
            Require.Positive(userId, nameof(userId));

            return _userRepository.GetUserById(userId);
        }

        public Task SaveUser(User userToSave)
        {
            Require.NotNull(userToSave, nameof(userToSave));

            return _userRepository.CreateUser(userToSave);
        }

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}