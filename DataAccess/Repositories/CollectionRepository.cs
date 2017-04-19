using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionService.Domain;
using CollectionService.Infrastructure;
using MongoDB.Bson;
using MongoDB.Driver;
using UserService.Infrastructure;

namespace DataAccess.Repositories
{
    public class CollectionRepository : ICollectionRepository
    {
        public Task SaveCollection(Collection collection)
        {
            var database = _session.GetDatabase("telegram");
            var collections = database.GetCollection<Collection>("collections");
            return collections.InsertOneAsync(collection);
        }

        public async Task<List<Collection>> GetCurrrentCollectionsByUserId(int userId)
        {
            var currentCollections = new List<Collection>();
            var user = await _userRepository.GetUserById(userId);
            for (var i = 1; i < user.Collections.Count; i++)
            {
                var collection = user.Collections[i];
                if (collection.Status)
                {
                    currentCollections.Add(collection);
                }
            }
            return currentCollections;
        }

        public async Task<List<Collection>> GetCompletedCollectionsByUserId(int userId)
        {
            var completedCollections = new List<Collection>();
            var user = await _userRepository.GetUserById(userId);
            for (var i = 1; i < user.Collections.Count; i++)
            {
                var collection = user.Collections[i];
                if (!collection.Status)
                {
                    completedCollections.Add(collection);
                }
            }
            return completedCollections;
        }

        public async Task<List<Collection>> GetAllCollectionsByUserId(int userId)
        {
            var allCollections = new List<Collection>();
            var user = await _userRepository.GetUserById(userId);
            for (var i = 0; i < user.Collections.Count; i++)
            {
                var collection = user.Collections[i];
                allCollections.Add(collection);
            }
            return allCollections;

        }

        public async Task UpdateCollection(Collection collection)
        {
            var database = _session.GetDatabase("telegram");
            var collections = database.GetCollection<Collection>("collections");
            var filter = Builders<Collection>.Filter.Eq("Id", collection._id);
            await collections.ReplaceOneAsync(filter, collection);
        }

        public async Task<Collection> GetCollectionById(string id)
        {
            var objectid = new ObjectId(id);
            var database = _session.GetDatabase("telegram");
            var collections = database.GetCollection<Collection>("collections");
            var filter = Builders<Collection>.Filter.Eq("_id", objectid);
            var allCollections = await collections.Find(filter).ToListAsync();
            Collection collection = null;
            foreach (var findcollection in allCollections)
            {
                collection = findcollection;
            }
            return collection;
        }

        private readonly MongoClient _session;

        private readonly IUserRepository _userRepository;

        public CollectionRepository(MongoClient session, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _session = session;
        }

       
    }
}
