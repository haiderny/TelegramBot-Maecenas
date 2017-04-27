using BotMain.Controllers;
using Journalist;
using Telegram.Bot.Args;

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
                    await _callbackController.CreateDonation(callbackQuery);
                    break;
                }

                case "currentDonation":
                {
                    await _callbackController.GetCurrentDonations(callbackQuery);
                    break;
                }
                    
                case "historyOfDonations":
                {
                    await _callbackController.GetAllDonations(callbackQuery);
                    break;
                }

                default:
                {
                    await _callbackController.GetViewCollection(callbackQuery);
                    break;
                }
            }
        }

        private static CallbackController _callbackController;

        public CallbackHandler(CallbackController callbackController)
        {
            _callbackController = callbackController;
        }
    }
}
