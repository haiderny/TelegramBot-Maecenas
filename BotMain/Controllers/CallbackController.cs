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
                var keyboard = new InlineKeyboardMarkup(new[] { new InlineKeyboardButton(collection.Target, collection.Id) });
                await BotMain.Bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"{Properties.Resources.Donation} {enumerator}",
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
                var keyboard = new InlineKeyboardMarkup(new[] { new InlineKeyboardButton(collection.Target, collection.Id) });
                await BotMain.Bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"{Properties.Resources.Donation} {enumerator}",
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
                collection.Target + Environment.NewLine +
                collection.Donation + Environment.NewLine + collection.Time);
        }

        private static IUserService _userService;
        private static ICollectionService _collectionService;

        public CallbackController(IUserService userService, ICollectionService collectionService)
        {
            _userService = userService;
            _collectionService = collectionService;
        }
    }
}
