using System;
using System.Threading.Tasks;
using DonationMessegeBuilder.Application;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotMain.Events
{
    public class CallbackHandler
    {
        public static async void BotOnReceivedCallback(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var callbackQuery = callbackQueryEventArgs.CallbackQuery;

            if (callbackQuery == null) return;

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

            Metka:
            if (MessegeHandler.Intermediate != String.Empty)
            {
                _messageBuilder.BuildTarget(MessegeHandler.Intermediate);
            }
            else
            {
                goto Metka;
            }

            await BotMain.Bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id,
                "Назовите сумму вашего пожертвования", replyMarkup: keyboard);

            Metka1:
            if (MessegeHandler.Intermediate != String.Empty)
            {
                _messageBuilder.BuildDonation(MessegeHandler.Intermediate);
            }
            else
            {
                goto Metka1;
            }


            await BotMain.Bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id,
                "Назовите сроки вашего пожертвования", replyMarkup: keyboard);

            Metka2:
            if (MessegeHandler.Intermediate != String.Empty)
            {
                _messageBuilder.BuildTime(MessegeHandler.Intermediate);
            }
            else
            {
                goto Metka2;
            }

            await BotMain.Bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, _messageBuilder.GetResult());
            
        }

        private static IMessageBuilder _messageBuilder;

        public CallbackHandler(IMessageBuilder messageBuilder)
        {
            _messageBuilder = messageBuilder;
        }
    }
}
