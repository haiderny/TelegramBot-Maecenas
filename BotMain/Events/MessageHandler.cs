using BotMain.Controllers;
using CollectionService.Domain;
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
                    await _messagesController.OnStartRoute(message);
                    break;
                case UserStatus.AddCreditCard:
                    await _messagesController.AddCreditCardByUserId(currentUser.Id, message);
                    break;
                case UserStatus.AddYandexPurse:
                    await _messagesController.AddYandexPurseByUserId(currentUser.Id, message);
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

