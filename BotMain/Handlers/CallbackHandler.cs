using BotMain.Controllers;
using DataAccess.Entities;
using Journalist;
using Telegram.Bot.Args;
using UserService.IService;

namespace BotMain.Handlers
{
    public class CallbackHandler
    {
        public static async void BotOnReceivedCallback(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var callbackQuery = callbackQueryEventArgs.CallbackQuery;

            Require.NotNull(callbackQuery, nameof(callbackQuery));

            if (callbackQuery.Data == "Pay")
            {
                await BotMain.Bot.AnswerCallbackQueryAsync(callbackQuery.Id, "Money");
                return;
            }

            var currentUser = await _userService.GetUserById(callbackQuery.From.Id);

            switch (currentUser.UserStatus)
            {
                case UserStatus.CloseDonation:
                {
                    await _callbackController.CloseCurrentDonation(callbackQuery, currentUser);
                    break;
                }
            }

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

                case "CloseDonation":
                {
                    currentUser.UserStatus = UserStatus.CloseDonation;
                    await _userService.UpdateUser(currentUser);
                    await _callbackController.GetCurrentDonations(callbackQuery);
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
        private static IUserService _userService;

        public CallbackHandler(CallbackController callbackController, IUserService userService)
        {
            _callbackController = callbackController;
            _userService = userService;
        }
    }
}
