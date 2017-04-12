﻿using CollectionService.Application;
using UserService.Application;
using UserService.Entities;

namespace BotMain.Controllers
{
    public class CollectionController
    {
        public async void AddTargetToCollection(User user, string name)
        {
            var builder = _collectionService.GetBuilderForCollection(user.Id);
            builder.AddTarget(name);
            user.UserStatus = UserStatus.Amount;
            await _userService.SaveUser(user);
        }

        public async void AddAmountToCollection(User user, int amount)
        {
            var builder = _collectionService.GetBuilderForCollection(user.Id);
            builder.AddAmount(amount);
            user.UserStatus = UserStatus.Time;
            await _userService.SaveUser(user);
        }

        public async void AddTimeToCollection(User user, string time)
        {
            var builder = _collectionService.GetBuilderForCollection(user.Id);
            builder.AddTime(time);
            user.UserStatus = UserStatus.New;
            await _userService.SaveUser(user);
        }

        private readonly ICollectionService _collectionService;
        private readonly IUserService _userService;

        public CollectionController(ICollectionService collectionService, IUserService userService)
        {
            _collectionService = collectionService;
            _userService = userService;
        }
    }
}