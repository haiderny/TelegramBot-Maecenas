using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollectionService.Application;
using CollectionService.Domain;
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
            var allCollections = await _collectionService.GetAllCollectionsByUserId(int.Parse(callbackQuery.From.Id));
            var collections = allCollections as IList<Collection> ?? allCollections.ToList();
            if (!collections.Any())
            {
                await BotMain.Bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id,
                    $"{Properties.Resources.NoDonations}");
            }
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

        public async Task GetCurrentDonations(CallbackQuery callbackQuery)
        {
            var collections = await _collectionService.GetCurrentCollectionsByUserId(int.Parse(callbackQuery.From.Id));
            var allCollections = collections as IList<Collection> ?? collections.ToList();
            if (!allCollections.Any())
            {
                await BotMain.Bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"{Properties.Resources.NoDonations}");
            }
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

        public async Task CloseCurrentDonation(CallbackQuery callbackQuery)
        {
            var collection = await _collectionService.GetCollectionById(callbackQuery.Data, int.Parse(callbackQuery.From.Id));
            collection.Status = false;
            await _collectionService.UpdateCollection(collection, int.Parse(callbackQuery.From.Id));
        }

        public async Task CreateDonation(CallbackQuery callbackQuery)
        {
            await BotMain.Bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id,
                $"{Properties.Resources.NewDonation}");
            var user = await _userService.GetUserById(int.Parse(callbackQuery.From.Id));
            user.UserStatus = UserStatus.Target;
            await _userService.UpdateUser(user);
        }

        public async Task GetViewCollection(CallbackQuery callbackQuery)
        {
            var collection = await _collectionService.GetCollectionById(callbackQuery.Data, int.Parse(callbackQuery.From.Id));
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    new InlineKeyboardButton($"{Properties.Resources.CloseDonationButton}",
                        $"{collection._id}")
                }
            });
            if (collection.Status)
            {
                await BotMain.Bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id,
                    $"{Properties.Resources.Target} {collection.Target + Environment.NewLine}" +
                    $"{Properties.Resources.Amount} {collection.Donation + Environment.NewLine}" +
                    $"{Properties.Resources.Time} {collection.Time + Environment.NewLine}" +
                    _collectionController.UpdateStatusBar(collection), replyMarkup: keyboard);
            }
            else
            {
                await BotMain.Bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id,
                    $"{Properties.Resources.Target} {collection.Target + Environment.NewLine}" +
                    $"{Properties.Resources.Amount} {collection.Donation + Environment.NewLine}" +
                    $"{Properties.Resources.Time} {collection.Time + Environment.NewLine}" +
                    _collectionController.UpdateStatusBar(collection) + $"{Environment.NewLine}" +
                    $"{Properties.Resources.CloseDonation}");
            }
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
