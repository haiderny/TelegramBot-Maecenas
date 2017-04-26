using System.Collections.Generic;
using System.Threading.Tasks;
using BotMain.Controllers;
using CollectionService.Domain;
using Journalist;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using UserService.Application;
using UserService.Entities;
using User = UserService.Entities.User;

namespace BotMain.Events
{
    public class MessageHandler
    {
        public static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            Require.NotNull(message, nameof(message));

            var currentUser = await _userService.GetUserById(message.From.Id);

            switch (message.Text)
            {
                case "/start":
                    {
                        currentUser = await MessagesController.RequireUser(message, currentUser);
                        await MessagesController.OnStartRoute(message);
                        break;
                    }
            }
            switch (currentUser.UserStatus)
            {
                case UserStatus.Target:
                    currentUser.Builder = new CollectionMessageBuilder();
                    _collectionController.AddTargetToCollection(currentUser, message);
                    break;
                case UserStatus.Amount:
                    _collectionController.AddAmountToCollection(currentUser, message);
                    break;
                case UserStatus.Time:
                    _collectionController.AddTimeToCollection(currentUser, message);
                    await MessagesController.OnStartRoute(message);
                    break;
            }
        }

        private static IUserService _userService;
        private static CollectionController _collectionController;

        public MessageHandler(IUserService userService, CollectionController collectionController)
        {
            _userService = userService;
            _collectionController = collectionController;
        }
    }
}

