using System.Threading.Tasks;
using MongoDB.Driver;

namespace DataAccess
{
    public static class MongoDriverExtentions
    {
        public static IMongoCollection<T> GetCollection<T>(this IMongoDatabase database)
        {
            return database.GetCollection<T>(typeof(T).Name);
        }
        
    }
}