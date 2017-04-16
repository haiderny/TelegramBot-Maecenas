using System.Threading.Tasks;
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
            for (var i = 1; i < user.Collections.Count; i++)
            {
                var collection = user.Collections[i];
                if (collection.Status)
                {
                    var keyboard = new InlineKeyboardMarkup(new [] {new InlineKeyboardButton(collection.Target), });
                    await BotMain.Bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "kek",
                replyMarkup: keyboard);
                }
            }
            
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

        public CallbackHandler(IUserService userService)
        {
            _userService = userService;
        }
    }
}
