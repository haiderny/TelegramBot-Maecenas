using CollectionService.Application;
using UserService.Application;
using UserService.Entities;

namespace BotMain.Controllers
{
    public class CollectionController
    {

        public async void AddTargetToCollection(User user, string name)
        {
            user.Builder.AddTarget(name);
            user.UserStatus = UserStatus.Amount;
            await _userService.UpdateUser(user);
        }

        public async void AddAmountToCollection(User user, int amount)
        {
            user.Builder.AddAmount(amount);
            user.UserStatus = UserStatus.Time;
            await _userService.UpdateUser(user);
        }

        public async void AddTimeToCollection(User user, string time)
        {
            user.Builder.AddTime(time);
            var collection = user.Builder.Build();
            user.Collections.Add(collection);
            user.UserStatus = UserStatus.New;
            await _userService.UpdateUser(user);
        }

        public async void BuildCollection(User user)
        {
            var collection = user.Builder.Build();
            user.Collections.Add(collection);
            user.UserStatus = UserStatus.New;
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