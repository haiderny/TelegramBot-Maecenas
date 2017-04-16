using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
using static System.Convert;
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
                    await BotMain.Bot.SendTextMessageAsync(message.Chat.Id, "Назовите сумму вашего пожертвования", replyMarkup: keyboard);
                    break;
                case UserStatus.Amount:
                    int amount;
                    if (!int.TryParse(message.Text, out amount))
                    {
                        await BotMain.Bot.SendTextMessageAsync(message.Chat.Id,
                            "Неверный формат данных. Введите целочисленное значение");
                    }
                    else
                    {
                        _collectionController.AddAmountToCollection(currentUser, amount);
                        await BotMain.Bot.SendTextMessageAsync(message.Chat.Id, "Назовите сроки вашего пожертвования",
                            replyMarkup: keyboard);
                    }
                    break;
                case UserStatus.Time:
                    _collectionController.AddTimeToCollection(currentUser, message.Text);
                    var collection = currentUser.Builder.Build();
                    currentUser.Collections.Add(collection);
                    await BotMain.Bot.SendTextMessageAsync(message.Chat.Id, $"Цель пожертвования: {collection.Target} {Environment.NewLine}" +
                                                                            $"Сумма пожертвования: {collection.Donation} {Environment.NewLine}" +
                                                                            $"Сроки пожертвования: {collection.Time} {Environment.NewLine} ");
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
                        new InlineKeyboardButton("Новое пожертвование", "newDonation"),
                        new InlineKeyboardButton("Текущее пожертвование", "currentDonation"),
                    },
                    new[]
                    {
                        new InlineKeyboardButton("История пожертвований", "historyOfDonations"),
                    }
                });

            await BotMain.Bot.SendTextMessageAsync(message.Chat.Id,
                $"Привет, {message.From.FirstName} {message.From.LastName} ,{Environment.NewLine} {Properties.Resources.Choose}", 
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

