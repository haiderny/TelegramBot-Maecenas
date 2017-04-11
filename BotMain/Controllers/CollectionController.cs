using CollectionService.Application;
using UserService.Application;
using UserService.Entities;

namespace BotMain.Controllers
{
    public class CollectionController
    {
        public async void AddTargetToCollection(User user, string name)
        {

            var builder = _collectionService.GetBuilderForCollection(user.Id);
            builder.AddTarget(name);
            user.UserStatus = UserStatus.Amount;
            await _userService.SaveUser(user);
        }

        private readonly ICollectionService _collectionService;
        private readonly IUserService _userService;

        public CollectionController(ICollectionService collectionService, IUserService userService)
        {
            _collectionService = collectionService;
            _userService = userService;
        }
    }
}