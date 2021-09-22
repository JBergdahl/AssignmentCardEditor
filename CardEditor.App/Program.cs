using System;
using CardEditor.Domain;
using Data;
using MongoDB.Driver;

namespace CardEditor.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new CardEditorDbContext();
            var cardCollection = dbContext.Cards;

            var cardTest = new Card { Name = "testCard" };
            cardCollection.InsertOne(cardTest);

            var filter = FilterDefinition<Card>.Empty;
            var allCards = cardCollection.Find(filter).ToList();

            foreach (var card in allCards)
            {
                Console.WriteLine($"{card.Name} is in collection");
            }
            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
