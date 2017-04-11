using Telegram.Bot;

namespace BotMain.Domain
{
    public class BotMain
    {
        public static TelegramBotClient Bot { get; set; }

        public BotMain(TelegramBotClient bot)
        {
            Bot = bot;
        }
    }
}
