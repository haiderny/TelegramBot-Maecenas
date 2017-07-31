using System.Threading.Tasks;
using DataAccess.Entities;

namespace UserService.IService
{
    public interface IUserService
    {
        Task<User> GetUserById(int userId);

        Task SaveUser(User userToSave);

        Task UpdateUser(User user);
    }
}