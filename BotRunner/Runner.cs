using System;
using BotMain.Events;
using BotMain.Handlers;

namespace BotRunner
{
    internal class Runner
    {
        public static void Main(string[] args)
        {
            var container = new SimpleInjector.Container();
            BootStrapper.Start(container);

            BotMain.BotMain.Bot.OnCallbackQuery += CallbackHandler.BotOnReceivedCallback;
            BotMain.BotMain.Bot.OnMessage += MessageHandler.BotOnMessageReceived;
            BotMain.BotMain.Bot.OnReceiveError += ErrorHandler.BotOnErrorReceived;
            BotMain.BotMain.Bot.OnInlineQuery += InlineQueryHandler.BotOnInlineQueryReceived;

            BotMain.BotMain.Bot.StartReceiving();
            Console.ReadLine();
            BotMain.BotMain.Bot.StopReceiving();

        }
    }
}