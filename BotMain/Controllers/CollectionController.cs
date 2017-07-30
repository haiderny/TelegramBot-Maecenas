using System;
using System.Threading.Tasks;
using CollectionService.Domain;
using Telegram.Bot.Types;
using UserService.Application;
using UserService.Entities;
using User = UserService.Entities.User;

namespace BotMain.Controllers
{
    public class CollectionController
    {

        public async void AddTargetToCollection(User user, Message message)
        {
            user.Builder = new CollectionMessageBuilder();
            user.Builder.AddTarget(message.Text);
            user.UserStatus = UserStatus.Amount;
            await _userService.UpdateUser(user);
            await BotMain.Bot.SendTextMessageAsync(message.Chat.Id, $"{Properties.Resources.AmountDonation}");
        }

        public async void AddAmountToCollection(User user, Message message)
        {
            int amount;
            if (!int.TryParse(message.Text, out amount))
            {
                await BotMain.Bot.SendTextMessageAsync(message.Chat.Id,
                    $"{Properties.Resources.TypeException}");
            }
            else
            {
                user.Builder.AddAmount(amount);
                user.UserStatus = UserStatus.Time;
                await _userService.UpdateUser(user);
                await BotMain.Bot.SendTextMessageAsync(message.Chat.Id, $"{Properties.Resources.TimeDonation}");
            }
        }
        
        public async void AddTimeToCollection(User user, Message message)
        {
            user.Builder.AddTime(message.Text);
            user.UserStatus = UserStatus.AddCardNumber;
            await _userService.UpdateUser(user);
            await BotMain.Bot.SendTextMessageAsync(message.Chat.Id, $"{Properties.Resources.WriteCreditCard}");
        }

        public async void AddCreditCard(User user, Message message)
        {

            ulong number;
            if (!ulong.TryParse(message.Text, out number))
            {
                await BotMain.Bot.SendTextMessageAsync(message.Chat.Id, $"{Properties.Resources.TypeException}");
            }
            else
            {
                user.Builder.AddNumberCard(number);
                user.UserStatus = UserStatus.New;
                var newCollection = user.Builder.Build();
                user.Collections.Add(newCollection);
                await BotMain.Bot.SendTextMessageAsync(message.Chat.Id,
                    $"{Properties.Resources.Target} {newCollection.Target + Environment.NewLine}" +
                    $"{Properties.Resources.Amount} {newCollection.Donation + Environment.NewLine}" +
                    $"{Properties.Resources.Time} {newCollection.Time + Environment.NewLine}" +
                    UpdateStatusBar(newCollection));
                user.Builder = new CollectionMessageBuilder();
                await _userService.UpdateUser(user);
                await _messagesController.OnStartRoute(message);
            }
        }
        public string UpdateStatusBar(Collection collection)
        {
            var statusBar = string.Empty;
            var status = collection.Amount / (collection.Donation / 10);
            if (collection.Amount == 0)
            {
                return statusBar + $"{Properties.Resources.UpdateEmptyStatusBar}";
            }
            else
            {
                statusBar += $"{Properties.Resources.UpdateStatusBar}" + Environment.NewLine +
                             $"{collection.Amount} / {collection.Donation}" + Environment.NewLine;
            }
            for (var i = 0; i < status && i < 10; i++)
            {
                statusBar += "\U0001F44D";
            }
            statusBar += $" {Math.Round(collection.Amount / ((float)collection.Donation / 100), 1)}%";
            return statusBar;
        }

        private readonly IUserService _userService;
        private readonly MessagesController _messagesController;

        public CollectionController(IUserService userService, MessagesController messagesController)
        {
            _userService = userService;
            _messagesController = messagesController;
        }
    }
}