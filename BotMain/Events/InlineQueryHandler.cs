using Telegram.Bot.Args;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotMain.Events
{
    public class InlineQueryHandler
    {
        public static async void BotOnInlineQueryReceived(object sender, InlineQueryEventArgs inlineQueryEventArgs)
        {
            var inlineQuery = inlineQueryEventArgs.InlineQuery;
            if (inlineQuery.Query == "message")
            {
                InlineQueryResult[] results =
                {
                    new InlineQueryResult
                    {
                        Id = "1",
                        InputMessageContent = new InputTextMessageContent {MessageText = "kek"},
                        Title = "lol"

                    },
                };
                await BotMain.Bot.AnswerInlineQueryAsync(inlineQuery.Id, results);
            }
        }
    }
}
