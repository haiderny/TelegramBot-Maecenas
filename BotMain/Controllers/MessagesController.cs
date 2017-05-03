using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionService.Domain;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using UserService.Application;
using UserService.Entities;
using User = UserService.Entities.User;

namespace BotMain.Controllers
{
    public class MessagesController
    {

        public async Task OnStartRoute(Message message)
        {
            var currentUser = await _userService.GetUserById(message.From.Id);
            currentUser.UserStatus = UserStatus.New;
            await _userService.UpdateUser(currentUser);
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

        public async Task<User> RequireUser(Message message)
        {
            var currentUser = await _userService.GetUserById(message.From.Id);
            if (currentUser == null)
            {
                currentUser = new User(message.From.Id, message.From.FirstName,
                    message.From.LastName, new List<Collection>(), UserStatus.New, new CollectionMessageBuilder());
                await _userService.SaveUser(currentUser);
            }
            return currentUser;
        }

        public async Task AddYandexPurseByUserId(int userId, Message message)
        {
            var user = await _userService.GetUserById(userId);
            int number;
            if (!int.TryParse(message.Text, out number))
            {
                await BotMain.Bot.SendTextMessageAsync(message.Chat.Id,
                    $"{Properties.Resources.TypeException}");
            }
            else
            {
                user.NumberYandexPurse = number;
                await _userService.UpdateUser(user);
                await BotMain.Bot.SendTextMessageAsync(message.Chat.Id, $"{Properties.Resources.SuccessfullyYandexPurse}");
                await _settingsController.GetProfileUser(message);

            }
        }

        public async Task AddCreditCardByUserId(int userId, Message message)
        {
            var user = await _userService.GetUserById(userId);
            int number;
            if (!int.TryParse(message.Text, out number))
            {
                await BotMain.Bot.SendTextMessageAsync(message.Chat.Id,
                    $"{Properties.Resources.TypeException}");
            }
            else
            {
                user.NumberCreditCard = number;
                await _userService.UpdateUser(user);
                await BotMain.Bot.SendTextMessageAsync(message.Chat.Id, $"{Properties.Resources.SuccessfullyCreditCard}");
                await _settingsController.GetProfileUser(message);
            }
        }

        private static IUserService _userService;
        private static ProfileSettingsController _settingsController;

        public MessagesController(IUserService userService, ProfileSettingsController settingsController)
        {
            _settingsController = settingsController;
            _userService = userService;
        }
    }
}
