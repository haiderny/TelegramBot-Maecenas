using BotMain.Events;
using DataAccess;
using DataAccess.Repositories;
using DonationMessegeBuilder;
using DonationMessegeBuilder.Application;
using DonationMessegeBuilder.Domain;
using MongoDB.Driver;
using SimpleInjector;
using Telegram.Bot;
using UserService.Application;
using UserService.Infrastructure;

namespace BotRunner
{
    public class BootStrapper
    {
        public static Container Start(Container container)
        {
            container.Register(() => new TelegramBotClient(Properties.Settings.Default.Token), Lifestyle.Singleton);
            container.Register<BotMain.BotMain>(Lifestyle.Singleton);
            container.Register<MessegeHandler>(Lifestyle.Singleton);
            container.Register<CallbackHandler>(Lifestyle.Singleton);
            container.Register<ErrorHandler>(Lifestyle.Singleton);
            container.Register<IMessageBuilder, MessageBuilder>(Lifestyle.Singleton);
            container.Register(() => new MongoClient(Properties.Settings.Default.ConnectionString), Lifestyle.Singleton);
            container.Register<IUserRepository, UserRepository>(Lifestyle.Singleton);
            container.Register<IUserService, UserService.Domain.UserService>(Lifestyle.Singleton);
            container.Verify();

            return container;
        }
    }
}
