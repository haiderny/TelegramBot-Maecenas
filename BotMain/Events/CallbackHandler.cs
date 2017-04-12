using System;
using System.Threading.Tasks;
using DonationMessegeBuilder.Application;
using Journalist;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using UserService.Application;
using UserService.Entities;
using UserService.Infrastructure;

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
                    break;
                }

                case "historyOfDonations":
                {
                    break;
                }
            }
        }

        private static async Task CreateDonation(CallbackQuery callbackQuery)
        {
            var keyboard = new ForceReply();

            await BotMain.Bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id,
                "Назовите цель своего пожертвование?", replyMarkup: keyboard);

            var user = await _userService.GetUserById(callbackQuery.From.Id);

            user.UserStatus = UserStatus.Target;
        }

        private static IUserService _userService;

        private static IMessageBuilder _messageBuilder;

        public CallbackHandler(IMessageBuilder messageBuilder, IUserService userService)
        {
            _messageBuilder = messageBuilder;
            _userService = userService;
        }
    }
}
