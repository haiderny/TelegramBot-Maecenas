using System;
using CollectionService.Application;
using Telegram.Bot.Types;
using UserService.Application;
using UserService.Entities;
using User = UserService.Entities.User;

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

        public async void AddTimeToCollection(User user, string time, Message message)
        {
            user.Builder.AddTime(time);
            var collection = user.Builder.Build();
            user.Collections.Add(collection);
            user.UserStatus = UserStatus.New;
            await _userService.UpdateUser(user);
            await _collectionService.CreateCollection(collection);
            await BotMain.Bot.SendTextMessageAsync(message.Chat.Id, $"{Properties.Resources.Target} {collection.Target} {Environment.NewLine}" +
                                                                            $"{Properties.Resources.Amount} {collection.Donation} {Environment.NewLine}" +
                                                                            $"{Properties.Resources.Time} {collection.Time} {Environment.NewLine} ");
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