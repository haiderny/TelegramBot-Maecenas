using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollectionService.Application;
using CollectionService.Domain;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using UserService.Application;
using static System.Decimal;
using User = UserService.Entities.User;

namespace BotMain.Controllers
{
    public class ProfileSettingsController
    {
        public async Task GetProfileUser(Message message)
        {
            var currentUser = await _userService.GetUserById(message.From.Id);
            var currentCollections = await _collectionService.GetCurrentCollectionsByUserId(message.From.Id);
            var allCollections = await _collectionService.GetAllCollectionsByUserId(message.From.Id);
            if (currentUser.NumberYandexPurse == Zero && currentUser.NumberCreditCard == Zero)
            {
                var keyboard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        new InlineKeyboardButton($"{Properties.Resources.AddYandexPurse}", $"{Properties.Resources.CallbackYandexPurse}")
                    },
                    new[]
                    {
                        new InlineKeyboardButton($"{Properties.Resources.AddCreditCard}", $"{Properties.Resources.CallbackCreditCard}")
                    }
                });

                await SendMessageWithoutPayment(message, currentUser, currentCollections, allCollections, keyboard);
            }
            else if (currentUser.NumberYandexPurse == Zero && currentUser.NumberCreditCard != Zero)
            {
                var keyboard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        new InlineKeyboardButton($"{Properties.Resources.AddYandexPurse}", $"{Properties.Resources.CallbackYandexPurse}")
                    }
                });
                await SendMessageWithYandexPurse(message, currentUser, currentCollections, allCollections, keyboard);
            }
            else if (currentUser.NumberYandexPurse != Zero && currentUser.NumberCreditCard == Zero)
            {
                var keyboard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        new InlineKeyboardButton($"{Properties.Resources.AddCreditCard}", $"{Properties.Resources.CallbackCreditCard}"),
                    }
                });
                await SendMessageWithCreditCard(message, currentUser, currentCollections, allCollections, keyboard);
            }
            else if (currentUser.NumberYandexPurse != Zero && currentUser.NumberCreditCard != Zero)
            {
                await SendMessageWithPayment(message, currentUser, currentCollections, allCollections);
            }

        }

        private static async Task SendMessageWithoutPayment(Message message, User currentUser, IEnumerable<Collection> currentCollections, IEnumerable<Collection> allCollections, IReplyMarkup keyboard)
        {
            await BotMain.Bot.SendTextMessageAsync(message.Chat.Id, $"{Properties.Resources.Name}: {currentUser.FirstName}{Environment.NewLine}" +
                                                                    $"{Properties.Resources.LastName}: {currentUser.LastName}{Environment.NewLine}" +
                                                                    $"{Properties.Resources.NumberCurrentCollections}: {currentCollections.Count()}{Environment.NewLine}" +
                                                                    $"{Properties.Resources.NumberAllCollections}: {allCollections.Count()}", replyMarkup: keyboard);
        }

        private static async Task SendMessageWithYandexPurse(Message message, User currentUser, IEnumerable<Collection> currentCollections, IEnumerable<Collection> allCollections, IReplyMarkup keyboard)
        {
            await BotMain.Bot.SendTextMessageAsync(message.Chat.Id, $"{Properties.Resources.Name}: {currentUser.FirstName}{Environment.NewLine}" +
                                                                    $"{Properties.Resources.LastName}: {currentUser.LastName}{Environment.NewLine}" +
                                                                    $"{Properties.Resources.NumberCurrentCollections}: {currentCollections.Count()}{Environment.NewLine}" +
                                                                    $"{Properties.Resources.NumberAllCollections}: {allCollections.Count()}{Environment.NewLine}" +
                                                                    $"{Properties.Resources.YandexPurse}: {currentUser.NumberYandexPurse}", replyMarkup: keyboard);
        }

        private static async Task SendMessageWithCreditCard(Message message, User currentUser, IEnumerable<Collection> currentCollections, IEnumerable<Collection> allCollections, IReplyMarkup keyboard)
        {
            await BotMain.Bot.SendTextMessageAsync(message.Chat.Id, $"{Properties.Resources.Name}: {currentUser.FirstName}{Environment.NewLine}" +
                                                                    $"{Properties.Resources.LastName}: {currentUser.LastName}{Environment.NewLine}" +
                                                                    $"{Properties.Resources.NumberCurrentCollections}: {currentCollections.Count()}{Environment.NewLine}" +
                                                                    $"{Properties.Resources.NumberAllCollections}: {allCollections.Count()}{Environment.NewLine}" +
                                                                    $"{Properties.Resources.CreditCard}: {currentUser.NumberCreditCard}", replyMarkup: keyboard);
        }

        private static async Task SendMessageWithPayment(Message message, User currentUser, IEnumerable<Collection> currentCollections, IEnumerable<Collection> allCollections)
        {
            await BotMain.Bot.SendTextMessageAsync(message.Chat.Id, $"{Properties.Resources.Name}: {currentUser.FirstName}{Environment.NewLine}" +
                                                                    $"{Properties.Resources.LastName}: {currentUser.LastName}{Environment.NewLine}" +
                                                                    $"{Properties.Resources.NumberCurrentCollections}: {currentCollections.Count()}{Environment.NewLine}" +
                                                                    $"{Properties.Resources.NumberAllCollections}: {allCollections.Count()}{Environment.NewLine}" +
                                                                    $"{Properties.Resources.CreditCard}: {currentUser.NumberCreditCard}{Environment.NewLine}" +
                                                                    $"{Properties.Resources.YandexPurse}: {currentUser.NumberYandexPurse}");
        }

        private static IUserService _userService;
        private static ICollectionService _collectionService;

        public ProfileSettingsController(IUserService userService, ICollectionService collectionService)
        {
            _collectionService = collectionService;
            _userService = userService;
        }
    }
}
