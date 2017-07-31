using BotMain.Controllers;
using DataAccess.Entities;
using Journalist;
using Telegram.Bot.Args;

namespace BotMain.Handlers
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
                    _collectionController.AddCreditCard(currentUser, message);
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

