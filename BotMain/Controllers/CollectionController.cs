using System;
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
            var collection = user.Builder.Build();
            user.Collections.Add(collection);
            user.UserStatus = UserStatus.New;
            await _userService.UpdateUser(user);
            await BotMain.Bot.SendTextMessageAsync(message.Chat.Id, $"{Properties.Resources.Target} {collection.Target} {Environment.NewLine}" +
                                                                    $"{Properties.Resources.Amount} {collection.Donation} {Environment.NewLine}" +
                                                                    $"{Properties.Resources.Time} {collection.Time} {Environment.NewLine} ");
        }

        private readonly IUserService _userService;

        public CollectionController(IUserService userService)
        {
            _userService = userService;
        }
    }
}