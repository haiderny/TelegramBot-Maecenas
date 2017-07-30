using Telegram.Bot;

namespace BotMain
{
    public class BotMain
    {
        public static TelegramBotClient Bot { get; private set; }

        public BotMain(TelegramBotClient bot)
        {
            Bot = bot;
        }
    }
}
