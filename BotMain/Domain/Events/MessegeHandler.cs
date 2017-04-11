using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionService.Domain;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using UserService.Domain;
using UserService.Infrastructure;

namespace BotMain.Domain.Events
{
    public class MessegeHandler
    {
        public static string Intermediate { get; set; }

        public static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.TextMessage) return;

            switch (message.Text)
            {
                case "/start":
                {
                    
                    var user = await GetUserById(message.From.Id);
                    if (user == null)
                    {
                        var newUser = new UserService.Domain.User(message.From.Id, message.From.FirstName,
                        message.From.LastName, new List<Collection>(), UserStatus.New);
                        CreateUser(newUser);
                    }
                    await OnStartRoute(message);
                    break;
                }
                case "/get":
                {
                    var user = await GetUserById(message.From.Id);
                    await BotMain.Bot.SendTextMessageAsync(message.Chat.Id, user.FirstName + " " + user.LastName);
                    break;
                }
                default:
                {
                    Intermediate = message.Text;
                    break;
                }
            }
        }

        private static async Task<UserService.Domain.User> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            return user;
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
                string.Format("Привет, {0} {1} ,{2} {3}", message.From.FirstName, message.From.LastName,
                    Environment.NewLine, Properties.Resources.Choose), replyMarkup: keyboard);
        }
        
        private static void CreateUser(UserService.Domain.User user)
        {
            _userRepository.CreateUser(user);
        }

        private static IUserRepository _userRepository;

        public MessegeHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}

