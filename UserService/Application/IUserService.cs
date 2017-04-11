using System.Threading.Tasks;
using UserService.Entities;

namespace UserService.Application
{
    public interface IUserService
    {
        Task<User> GetUserById(int userId);

        Task SaveUser(User userToSave);
        //Save, some manipulations
    }
}