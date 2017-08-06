using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollectionService.Interfaces;
using DataAccess.Entities;
using Telegram.Bot.Types;
using UserService.IService;
using User = DataAccess.Entities.User;

namespace BotMain.Controllers
{
    public class ProfileSettingsController
    {
        public async Task GetProfileUser(Message message)
        {
            var currentUser = await _userService.GetUserById(message.From.Id);
            currentUser.UserStatus = UserStatus.New;
            await _userService.UpdateUser(currentUser);
            var currentCollections = await _collectionService.GetCurrentCollectionsByUserId(message.From.Id);
            var allCollections = await _collectionService.GetAllCollectionsByUserId(message.From.Id);

            await SendMessageProfile(message, currentUser, currentCollections, allCollections);
        }

        private static async Task SendMessageProfile(Message message, User currentUser, IEnumerable<Collection> currentCollections, IEnumerable<Collection> allCollections)
        {
            await BotMain.Bot.SendTextMessageAsync(message.Chat.Id, $"{Properties.Resources.Name}: {currentUser.FirstName}{Environment.NewLine}" +
                                                                    $"{Properties.Resources.LastName}: {currentUser.LastName}{Environment.NewLine}" +
                                                                    $"{Properties.Resources.NumberCurrentCollections}: {currentCollections.Count()}{Environment.NewLine}" +
                                                                    $"{Properties.Resources.NumberAllCollections}: {allCollections.Count()}");
        }

        private static IUserService _userService;
        private static ICollectionService _collectionService;

        public ProfileSettingsController(IUserService userService, ICollectionService collectionService)
        {
            _collectionService = collectionService;
            _userService = userService;
        }
    }
}
