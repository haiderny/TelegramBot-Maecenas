using System.Threading.Tasks;
using CollectionService.Application;
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
                    await GetCurrentDonation(callbackQuery);
                    break;
                }

                case "historyOfDonations":
                {
                    break;
                }
            }
        }

        private static async Task GetCurrentDonation(CallbackQuery callbackQuery)
        {
            var user = await _userService.GetUserById(callbackQuery.From.Id);
            var enumerator = 1;
            for (var i = 1; i < user.Collections.Count; i++)
            {
                var collection = user.Collections[i];
                if (!collection.Status) continue;
                var keyboard = new InlineKeyboardMarkup(new [] {new InlineKeyboardButton(collection.Target)});
                await BotMain.Bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "пожертвование " + enumerator,
                    replyMarkup: keyboard);
                enumerator++;
            }
            enumerator = 0;

        }

        private static async Task CreateDonation(CallbackQuery callbackQuery)
        {
            await BotMain.Bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id,
                "Назовите цель своего пожертвование?");

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
