﻿using BotMain.Controllers;
using Journalist;
using Telegram.Bot.Args;
using UserService.Entities;

namespace BotMain.Events
{
    public class MessageHandler
    {
        public static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            Require.NotNull(message, nameof(message));

            var currentUser = await _messagesController.RequireUser(message);

            switch (message.Text)
            {
                case "/start":
                    await _messagesController.OnStartRoute(message);
                    break;
                case "/profile":
                    await _profileSettings.GetProfileUser(message);
                    break;
            }
            switch (currentUser.UserStatus)
            {
                case UserStatus.Target:
                    _collectionController.AddTargetToCollection(currentUser, message);
                    break;
                case UserStatus.Amount:
                    _collectionController.AddAmountToCollection(currentUser, message);
                    break;
                case UserStatus.Time:
                    _collectionController.AddTimeToCollection(currentUser, message);
                    break;
                case UserStatus.AddCardNumber:
                    _collectionController.AddCreditCardNumber(currentUser, message);
                    break;
            }
        }

        private static MessagesController _messagesController;
        private static CollectionController _collectionController;
        private static ProfileSettingsController _profileSettings;

        public MessageHandler(CollectionController collectionController, MessagesController messagesController, ProfileSettingsController profileSettings)
        {
            _profileSettings = profileSettings;
            _messagesController = messagesController;
            _collectionController = collectionController;
        }
    }
}

