using BotMain.Controllers;
using BotMain.Events;
using CollectionService.Application;
using CollectionService.Domain;
using CollectionService.Infrastructure;
using DataAccess;
using DataAccess.Repositories;
using MongoDB.Driver;
using SimpleInjector;
using Telegram.Bot;
using UserService.Application;
using UserService.Entities;
using UserService.Infrastructure;

namespace BotRunner
{
    public class BootStrapper
    {
        public static Container Start(Container container)
        {
            var mongoClient = new MongoClient(Properties.Settings.Default.ConnectionString);
            container.Register(() => new TelegramBotClient(Properties.Settings.Default.Token), Lifestyle.Singleton);
            container.Register<BotMain.BotMain>(Lifestyle.Singleton);
            container.Register<MessageHandler>(Lifestyle.Singleton);
            container.Register<CallbackHandler>(Lifestyle.Singleton);
            container.Register<ErrorHandler>(Lifestyle.Singleton);
            container.Register(() => mongoClient, Lifestyle.Singleton);
            container.Register<IUserRepository, UserRepository>(Lifestyle.Singleton);
            container.Register<IUserService, UserService.Domain.UserService>(Lifestyle.Singleton);
            container.Register<ICollectionService, CollectionService.Domain.CollectionService>(Lifestyle.Singleton);
            container.Register<ICollectionMessageBuilder, CollectionMessageBuilder>(Lifestyle.Singleton);
            container.Register<CollectionController>(Lifestyle.Singleton);
            container.Register<ICollectionRepository, CollectionRepository>(Lifestyle.Singleton);
            container.Register<MessagesController>(Lifestyle.Singleton);
            container.Register<CallbackController>(Lifestyle.Singleton);
            container.Register<ProfileSettingsController>(Lifestyle.Singleton);

            container = RegisterCollections(container, mongoClient);
            container.Verify();

            return container;
        }

        private static Container RegisterCollections(Container container, MongoClient mongoClient)
        {
            var session = mongoClient;
            var database = session.GetDatabase($"{Properties.Settings.Default.DbName}");
            var users = database.GetCollection<User>();

            container.Register(() => users, Lifestyle.Singleton);
            return container;
        }
    }
}
