using System;
using System.Threading.Tasks;
using CollectionService.Application;
using CollectionService.Domain;
using Journalist;
using Journalist.Collections;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using UserService.Application;
using UserService.Entities;

namespace BotMain.Events
{
    public class CallbackHandler
    {
        public static async void BotOnReceivedCallback(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var callbackQuery = callbackQueryEventArgs.CallbackQuery;

            Require.NotNull(callbackQuery, nameof(callbackQuery));

            switch (callbackQuery.Data)
            {
                case "newDonation":
                {
                    await CreateDonation(callbackQuery);
                    break;
                }

                case "currentDonation":
                {
                    await GetCurrentDonations(callbackQuery);
                    break;
                }
                    
                case "historyOfDonations":
                {
                    await GetAllDonations(callbackQuery);
                    break;
                }

                default:
                {
                    var collection = await _collectionService.GetCollectionById(callbackQuery.Data);
                    await BotMain.Bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id,
                        collection.Target + Environment.NewLine +
                        collection.Donation + Environment.NewLine + collection.Time);
                    break;
                }
            }
        }

        private static async Task GetAllDonations(CallbackQuery callbackQuery)
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

        private static async Task GetCurrentDonations(CallbackQuery callbackQuery)
        {
            var user = await _userService.GetUserById(callbackQuery.From.Id);
            var enumerator = 1;
            foreach (var collection in user.Collections)
            {
                if (!collection.Status) continue;
                var keyboard = new InlineKeyboardMarkup(new [] {new InlineKeyboardButton(collection.Target, collection.Id)});
                await BotMain.Bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"{Properties.Resources.Donation} {enumerator}",
                    replyMarkup: keyboard);
                enumerator++;
            }
        }

        private static async Task CreateDonation(CallbackQuery callbackQuery)
        {
            await BotMain.Bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id,
                $"{Properties.Resources.NewDonation}");

            var user = await _userService.GetUserById(callbackQuery.From.Id);

            user.UserStatus = UserStatus.Target;

            await _userService.UpdateUser(user);
        }

        private static IUserService _userService;

        private static ICollectionService _collectionService;

        public CallbackHandler(IUserService userService, ICollectionService collectionService)
        {
            _collectionService = collectionService;
            _userService = userService;
        }
    }
}
