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
       

        public static async Task OnStartRoute(Message message)
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
        public static async Task<User> RequireUser(Message message, User currentUser)
        {
            if (currentUser == null)
            {
                var newUser = new User(message.From.Id, message.From.FirstName,
                    message.From.LastName, new List<Collection>(), UserStatus.New, new CollectionMessageBuilder());
                await _userService.SaveUser(newUser);
                currentUser = newUser;
            }
            currentUser.UserStatus = UserStatus.New; ;
            await _userService.UpdateUser(currentUser);
            return currentUser;
        }

        private static IUserService _userService;

        public MessagesController(IUserService userService)
        {
            _userService = userService;
        }
    }
}
