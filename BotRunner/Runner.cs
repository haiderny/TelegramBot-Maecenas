using System;
using BotMain.Domain.Events;
using Telegram.Bot.Args;

namespace BotRunner
{
    internal class Runner
    {
        public static void Main(string[] args)
        {
            var container = new SimpleInjector.Container();
            BootStrapper.Start(container);

            BotMain.Domain.BotMain.Bot.OnCallbackQuery += CallbackHandler.BotOnReceivedCallback;
            BotMain.Domain.BotMain.Bot.OnMessage += MessegeHandler.BotOnMessageReceived;
            BotMain.Domain.BotMain.Bot.OnReceiveError += ErrorHandler.BotOnErrorReceived;

            BotMain.Domain.BotMain.Bot.StartReceiving();
            Console.ReadLine();
            BotMain.Domain.BotMain.Bot.StopReceiving();

        }
    }
}