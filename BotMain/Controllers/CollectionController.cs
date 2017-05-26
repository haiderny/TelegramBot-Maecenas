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

        public async void AddCreditCardNumber(User user, Message message)
        {
            double number;
            if (!double.TryParse(message.Text, out number))
            {
                await BotMain.Bot.SendTextMessageAsync(message.Chat.Id,
                    $"{Properties.Resources.TypeException}");
            }
            else
            {
                user.Builder.AddNumberCard(number);
                user.UserStatus = UserStatus.New;
                var collection = user.Builder.Build();
                user.Collections.Add(collection);
                await _userService.UpdateUser(user);
                await BotMain.Bot.SendTextMessageAsync(message.Chat.Id,
                    $"{Properties.Resources.Target} {collection.Target} {Environment.NewLine}" +
                    $"{Properties.Resources.Amount} {collection.Donation} {Environment.NewLine}" +
                    $"{Properties.Resources.Time} {collection.Time} {Environment.NewLine}" +
                    $"{UpdateStatusBar(collection)}{Environment.NewLine}" +
                    $"{Properties.Resources.Continue}");
            }
        }

        public string UpdateStatusBar(Collection collection)
        {
            var statusBar = string.Empty;
            var status = collection.Amount / (collection.Donation / 10);
            if (collection.Amount == 0)
            {
                return statusBar += $"{Properties.Resources.UpdateEmptyStatusBar}";
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

        public CollectionController(IUserService userService)
        {
            _userService = userService;
        }
    }
}