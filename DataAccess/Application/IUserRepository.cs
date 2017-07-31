using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Application
{
    public interface IUserRepository
    {
        Task CreateUser(User user);

        Task<User> GetUserById(int id);

        Task UpdateUser(User user);
    }
}
