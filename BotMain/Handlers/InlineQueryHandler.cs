using System;
using System.Collections.Generic;
using System.Linq;
using BotMain.Controllers;
using CollectionService.Interfaces;
using DataAccess.Entities;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using Telegram.Bot.Types.ReplyMarkups;
using UserService.IService;

namespace BotMain.Handlers
{
    public class InlineQueryHandler
    {
        public static async void BotOnInlineQueryReceived(object sender, InlineQueryEventArgs inlineQueryEventArgs)
        {
            var inlineQuery = inlineQueryEventArgs.InlineQuery;
            switch (inlineQuery.Query)
            {
                case "/current":
                    var allCollection = await _collectionService.GetCurrentCollectionsByUserId(inlineQuery.From.Id);
                    var collections = allCollection as IList<Collection> ?? allCollection.ToList();
                    var results = new InlineQueryResult[collections.Count()];
                    var enumerator = 0;
                    foreach (var collection in collections)
                    {
                        var keyboard = new InlineKeyboardMarkup(new []{new InlineKeyboardCallbackButton("Внести средства", "Pay"), });
                        var result = new InlineQueryResultArticle
                        {
                            Id = collection._id,
                            Title = collection.Target,
                            ReplyMarkup = keyboard
                        };
                        InputMessageContent message = new InputTextMessageContent
                        {
                            MessageText = $"{Properties.Resources.Target} {collection.Target}{Environment.NewLine}" +
                                          $"{Properties.Resources.Amount} {collection.Amount}{Environment.NewLine}" +
                                          $"{Properties.Resources.Time} {collection.Time}{Environment.NewLine}" +
                                          $"{Properties.Resources.UpdateStatusBar} {_collectionController.UpdateStatusBar(collection)}"
                        };
                        result.InputMessageContent = message;
                    
                        results[enumerator] = result;
                        enumerator++;

                    }
                    await BotMain.Bot.AnswerInlineQueryAsync(inlineQueryEventArgs.InlineQuery.Id, results, isPersonal: true,
                        cacheTime: 0);
                    break;
            }
        }

        private static IUserService _userService;
        private static CollectionController _collectionController;
        private static ICollectionService _collectionService;


        public InlineQueryHandler(IUserService userService, ICollectionService collectionService, CollectionController collectionController)
        {
            _collectionController = collectionController;
            _userService = userService;
            _collectionService = collectionService;
        }
    }
}
