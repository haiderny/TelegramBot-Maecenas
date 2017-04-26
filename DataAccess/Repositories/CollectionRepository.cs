using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CollectionService.Domain;
using CollectionService.Infrastructure;
using Journalist;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using UserService.Entities;
using UserService.Infrastructure;

namespace DataAccess.Repositories
{
    public class CollectionRepository : ICollectionRepository
    {
        public Task SaveCollection(Collection collection)
        {
            return _donationCollection.InsertOneAsync(collection);
        }

        public async Task<IEnumerable<Collection>> GetCurrrentCollectionsByUserId(int userId)
        {
            Require.Positive(userId, nameof(userId));

            var user = await _userRepository.GetUserById(userId);
            return user.Collections.Where(collection => collection.Status);
        }

        public async Task<IEnumerable<Collection>> GetCompletedCollectionsByUserId(int userId)
        {
            Require.Positive(userId, nameof(userId));

            var user = await _userRepository.GetUserById(userId);
            
            return user.Collections.Where(collection => !collection.Status);
        }

        public async Task<IEnumerable<Collection>> GetAllCollectionsByUserId(int userId)
        {
            Require.Positive(userId, nameof(userId));

            var user = await _userRepository.GetUserById(userId);
            return user.Collections;

        }

        public async Task UpdateCollection(Collection collection)
        {
            Require.NotNull(collection, nameof(collection));

            var filter = Builders<Collection>.Filter.Eq("Id", collection.Id);
            await _donationCollection.ReplaceOneAsync(filter, collection);
        }

        public async Task<IEnumerable<Collection>> GetCollections(Expression<Func<Collection, bool>> predicate = null)
        {
            return predicate != null ?
            await _donationCollection.Find(predicate).ToListAsync() :
            await _donationCollection.Find(c => true).ToListAsync();
        }

        public async Task<Collection> GetCollectionById(string id)
        {
            Require.NotEmpty(id, nameof(id));

            return await _donationCollection.AsQueryable().SingleOrDefaultAsync(collection => collection.Id == id);
        }

        private readonly IMongoCollection<Collection> _donationCollection;
        private readonly IUserRepository _userRepository;

        public CollectionRepository(IUserRepository userRepository, IMongoCollection<Collection> donationCollection)
        {
            _userRepository = userRepository;
            _donationCollection = donationCollection;
        }

       
    }
}
