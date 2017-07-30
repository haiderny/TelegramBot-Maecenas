using BotMain.Controllers;
using Journalist;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;
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
                case "/pay":
                {
                    var button = new InlineKeyboardButton("hui", "hui");
                    button.Pay = true;
                    var keyboard = new InlineKeyboardMarkup(new[]
                        {
                            button
                        }
                    );
                    await BotMain.Bot.SendTextMessageAsync(message.Chat.Id, "Message", replyMarkup: keyboard);
                    break;
                }
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

