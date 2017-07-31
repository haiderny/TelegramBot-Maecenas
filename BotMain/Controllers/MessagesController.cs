using System;
using System.Threading.Tasks;
using DataAccess.Entities;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;
using UserService.IService;
using User = DataAccess.Entities.User;

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
                    new InlineKeyboardCallbackButton($"{Properties.Resources.NewDonationButton}", 
                                                    $"{Properties.Resources.NewCallbackDonation}"),  
                },
                new []
                {
                    new InlineKeyboardCallbackButton($"{Properties.Resources.CurrentDonationButton}",
                                                    $"{Properties.Resources.CallbackCurrentDonation}"), 
                },
                new[]
                {
                    new InlineKeyboardCallbackButton($"{Properties.Resources.HistoryOfDonationsButton}", 
                                                    $"{Properties.Resources.CallbackHistoryOfDonations}"), 
                },
                new []
                {
                    new InlineKeyboardCallbackButton($"{Properties.Resources.CloseDonationButton}",
                                                    $"{Properties.Resources.CloseDonationCallback}"),   
                }
            });
            await BotMain.Bot.SendTextMessageAsync(message.Chat.Id,
                $"{Properties.Resources.Hello} {message.From.FirstName} {message.From.LastName} ," +
                $"{Environment.NewLine} {Properties.Resources.Choose}",
                replyMarkup: keyboard);
        }

        public async Task<User> RequireUser(Message message)
        {
            var currentUser = await _userService.GetUserById(message.From.Id);
            if (currentUser == null)
            {
                currentUser = new User(message.From.Id, message.From.FirstName,
                    message.From.LastName);
                await _userService.SaveUser(currentUser);
            }
            return currentUser;
        }

        private static IUserService _userService;

        public MessagesController(IUserService userService)
        {
            _userService = userService;
        }
    }
}
