﻿using BotMain.Controllers;
using Journalist;
using Telegram.Bot.Args;
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

            var currentUser = await _userService.GetUserById(callbackQuery.From.Id);

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
        private static IUserService _userService;

        public CallbackHandler(CallbackController callbackController, IUserService userService)
        {
            _callbackController = callbackController;
            _userService = userService;
        }
    }
}
