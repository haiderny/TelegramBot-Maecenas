﻿using System;
using BotMain.Events;

namespace BotRunner
{
    internal class Runner
    {
        public static void Main(string[] args)
        {
            var container = new SimpleInjector.Container();
            BootStrapper.Start(container);

            BotMain.BotMain.Bot.OnCallbackQuery += CallbackHandler.BotOnReceivedCallback;
            BotMain.BotMain.Bot.OnMessage += MessegeHandler.BotOnMessageReceived;
            BotMain.BotMain.Bot.OnReceiveError += ErrorHandler.BotOnErrorReceived;

            BotMain.BotMain.Bot.StartReceiving();
            Console.ReadLine();
            BotMain.BotMain.Bot.StopReceiving();

        }
    }
}