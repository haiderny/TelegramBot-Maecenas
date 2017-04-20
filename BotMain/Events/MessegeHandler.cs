using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotMain.Controllers;
using CollectionService.Application;
using CollectionService.Domain;
using Journalist;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using UserService.Application;
using UserService.Entities;
using User = UserService.Entities.User;

namespace BotMain.Events
{
    public class MessegeHandler
    {
        public static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            var keyboard = new ForceReply();

            Require.NotNull(message, nameof(message));

            var currentUser = await _userService.GetUserById(message.From.Id);

            switch (message.Text)
            {
                case "/start":
                {
                    if (currentUser == null)
                    {
                        var newUser = new User(message.From.Id, message.From.FirstName,
                            message.From.LastName, new List<Collection>(), UserStatus.New, new CollectionMessageBuilder());
                        await _userService.SaveUser(newUser);
                        currentUser = newUser;
                    }
                    currentUser.UserStatus = UserStatus.New;;
                    await OnStartRoute(message);
                    break;
                }
            }

            switch (currentUser.UserStatus)
            {
                case UserStatus.Target:
                    currentUser.Builder = new CollectionMessageBuilder();
                    _collectionController.AddTargetToCollection(currentUser, message.Text);
                    await BotMain.Bot.SendTextMessageAsync(message.Chat.Id, $"{Properties.Resources.AmountDonation}", replyMarkup: keyboard);
                    break;
                case UserStatus.Amount:
                    int amount;
                    if (!int.TryParse(message.Text, out amount))
                    {
                        await BotMain.Bot.SendTextMessageAsync(message.Chat.Id,
                            $"{Properties.Resources.TypeException}");
                    }
                    else
                    {
                        _collectionController.AddAmountToCollection(currentUser, amount);
                        await BotMain.Bot.SendTextMessageAsync(message.Chat.Id, $"{Properties.Resources.TimeDonation}",
                            replyMarkup: keyboard);
                    }
                    break;
                case UserStatus.Time:
                    _collectionController.AddTimeToCollection(currentUser, message.Text, message);
                    await OnStartRoute(message);
                    break;
            }
        }
        
        private static async Task OnStartRoute(Message message)
        {

            await BotMain.Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

            var keyboard = new InlineKeyboardMarkup(new[]
            {
                    new[]
                    {
                        new InlineKeyboardButton($"{Properties.Resources.NewDonationButton}", $"{Properties.Resources.NewCallbackDonation}"),
                        new InlineKeyboardButton($"{Properties.Resources.CurrentDonationButton}", $"{Properties.Resources.CallbackCurrentDonation}"),
                    },
                    new[]
                    {
                        new InlineKeyboardButton($"{Properties.Resources.HistoryOfDonationsButton}", $"{Properties.Resources.CallbackHistoryOfDonations}"),
                    }
                });

            await BotMain.Bot.SendTextMessageAsync(message.Chat.Id,
                $"{Properties.Resources.Hello} {message.From.FirstName} {message.From.LastName} ,{Environment.NewLine} {Properties.Resources.Choose}", 
                replyMarkup: keyboard);
        }

        private static IUserService _userService;
        private static ICollectionService _collectionService;
        private static CollectionController _collectionController;

        public MessegeHandler(IUserService userService, ICollectionService collectionService, CollectionController collectionController)
        {
            _userService = userService;
            _collectionService = collectionService;
            _collectionController = collectionController;
        }
    }
}

