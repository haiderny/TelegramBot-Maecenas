using System.Threading.Tasks;
using UserService.Domain;

namespace UserService.Infrastructure
{
    public interface IUserRepository
    {
        void CreateUser(User user);

        Task<User> GetUserById(int id);
    }
}
