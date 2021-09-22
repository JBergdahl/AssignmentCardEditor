using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardEditor.Domain;
using MongoDB.Driver;

namespace Data
{
    public class CardEditorDbContext : ICardEditorDbContext
    {
        private const string MongoDbUrl = "mongodb://localhost:27017";
        private const string CardEditorDbName = "CardEditor";

        // Store collection name
        private const string CardCollectionName = "cards";

        private readonly IMongoDatabase _database;

        public CardEditorDbContext(IMongoDatabase database)
        {
            _database = database;
        }


        // Store collection according to type
        private readonly Dictionary<Type, string> _collectionNames = new()
        {
            { typeof(Card), CardCollectionName }
        };

        // Get collection that corresponds to the type
        private IMongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(_collectionNames[typeof(T)]);
        }

        public IMongoCollection<Card> Cards => GetCollection<Card>();
    }
}
