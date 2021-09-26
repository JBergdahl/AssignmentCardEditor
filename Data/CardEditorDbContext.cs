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
        // Store collection names
        private const string CardCollectionName = "cards";
        private const string CardTypeCollectionName = "types";

        private readonly IMongoDatabase _database;

        public CardEditorDbContext(IMongoDatabase database)
        {
            _database = database;
        }


        // Store collection according to type
        private readonly Dictionary<Type, string> _collectionNames = new()
        {
            { typeof(Card), CardCollectionName },
            { typeof(CardType), CardTypeCollectionName }
        };

        // Get collection that corresponds to the type
        private IMongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(_collectionNames[typeof(T)]);
        }

        public IMongoCollection<Card> Cards => GetCollection<Card>();
        public IMongoCollection<CardType> CardTypes => GetCollection<CardType>();

        public async Task ClearDataBase()
        {
            var tasks = new List<Task>();
            foreach (var collectionName in _collectionNames)
            {
                var dropTask = Task.Run(() => _database.DropCollectionAsync(collectionName.Value));
                tasks.Add(dropTask);
            }

            await Task.WhenAll(tasks);
        }
    }
}
