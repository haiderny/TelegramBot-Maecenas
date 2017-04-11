using System.Diagnostics;
using Telegram.Bot.Args;

namespace BotMain.Domain.Events
{
    public class ErrorHandler
    {
        public static void BotOnErrorReceived(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Debugger.Break();
        }
    }
}
