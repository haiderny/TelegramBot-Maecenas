using System.Threading.Tasks;
using UserService.Entities;

namespace UserService.Infrastructure
{
    public interface IUserRepository
    {
        Task CreateUser(User user);

        Task<User> GetUserById(int id);

        Task UpdateUser(User user);
    }
}
