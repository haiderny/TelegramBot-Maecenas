using System.Diagnostics;
using Telegram.Bot.Args;

namespace BotMain.Handlers
{
    public class ErrorHandler
    {
        public static void BotOnErrorReceived(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Debugger.Break();
        }
    }
}
