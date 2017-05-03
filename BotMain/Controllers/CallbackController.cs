using System;
using System.Threading.Tasks;
using CollectionService.Application;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using UserService.Application;
using UserService.Entities;

namespace BotMain.Controllers
{
    public class CallbackController
    {
        public async Task GetAllDonations(CallbackQuery callbackQuery)
        {
            var allCollections = await _collectionService.GetAllCollectionsByUserId(callbackQuery.From.Id);
            var enumerator = 1;
            foreach (var collection in allCollections)
            {
                if (collection == null) continue;
                var keyboard = new InlineKeyboardMarkup(new[] { new InlineKeyboardButton(collection.Target, collection._id) });
                await BotMain.Bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"{Properties.Resources.Donation} {enumerator}:",
                    replyMarkup: keyboard);
                enumerator++;
            }
        }

        public async Task GetCurrentDonations(CallbackQuery callbackQuery)
        {
            var collections = await _collectionService.GetCurrentCollectionsByUserId(callbackQuery.From.Id);
            var enumerator = 1;
            foreach (var collection in collections)
            {
                if (collection == null) continue;
                var keyboard = new InlineKeyboardMarkup(new[] { new InlineKeyboardButton(collection.Target, collection._id) });
                await BotMain.Bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"{Properties.Resources.Donation} {enumerator}:",
                    replyMarkup: keyboard);
                enumerator++;
            }
        }

        public async Task CreateDonation(CallbackQuery callbackQuery)
        {
            await BotMain.Bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id,
                $"{Properties.Resources.NewDonation}");
            var user = await _userService.GetUserById(callbackQuery.From.Id);
            user.UserStatus = UserStatus.Target;
            await _userService.UpdateUser(user);
        }

        public async Task GetViewCollection(CallbackQuery callbackQuery)
        {
            var collection = await _collectionService.GetCollectionById(callbackQuery.Data, callbackQuery.From.Id);
            await BotMain.Bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id,
                $"{Properties.Resources.Target}: {collection.Target + Environment.NewLine}" +
                $"{Properties.Resources.Donation}: {collection.Donation + Environment.NewLine}" + 
                $"{Properties.Resources.Time}: {collection.Time + Environment.NewLine}" +
                _collectionController.UpdateStatusBar(collection));
        }

        private static IUserService _userService;
        private static ICollectionService _collectionService;
        private static CollectionController _collectionController;

        public CallbackController(IUserService userService, ICollectionService collectionService, CollectionController collectionController)
        {
            _collectionController = collectionController;
            _userService = userService;
            _collectionService = collectionService;
        }
    }
}
