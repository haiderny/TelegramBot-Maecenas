using System.Threading.Tasks;
using DataAccess.Application;
using DataAccess.Entities;
using Journalist;
using UserService.IService;


namespace UserService.Domain
{
    public class UserService : IUserService
    {
        public Task<User> GetUserById(int userId)
        {
            Require.Positive(userId, nameof(userId));

            return _userRepository.GetUserById(userId);
        }

        public Task UpdateUser(User userToUpdate)
        {
            Require.NotNull(userToUpdate, nameof(userToUpdate));

            return _userRepository.UpdateUser(userToUpdate);
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