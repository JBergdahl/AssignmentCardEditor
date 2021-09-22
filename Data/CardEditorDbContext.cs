using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardEditor.Domain;
using MongoDB.Driver;

namespace Data
{
    public class CardEditorDbContext
    {
        private const string MongoDbUrl = "mongodb://localhost:27017";
        private const string CardEditorDbName = "CardEditor";
        private readonly IMongoDatabase _database;

        public CardEditorDbContext()
        {
            var client = new MongoClient(MongoDbUrl);
            _database = client.GetDatabase(CardEditorDbName);
        }

        // Store collection name
        private const string CardCollectionName = "cards";

        // Store collection according to type
        private readonly Dictionary<Type, string> _collectionNames = new()
        {
            { typeof(Card), CardCollectionName }
        };

        public IMongoCollection<Card> Cards => GetCollection<Card>();

        // Get collection that corresponds to the type
        private IMongoCollection<T> GetCollection<T>()
        {
            var typeOfInstance = typeof(T);
            var collectionName = _collectionNames[typeOfInstance];
            return _database.GetCollection<T>(collectionName);
        }
    }
}
