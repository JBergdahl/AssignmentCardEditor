using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardEditor.Domain;
using MongoDB.Driver;

namespace Data
{
    public class DbMethods : IDbMethods
    {
        private readonly ICardEditorDbContext _context;

        public DbMethods(ICardEditorDbContext context)
        {
            _context = context;
        }

        public async Task<Card> AddOneCard(string cardName, int attack, int defense, int speed, int mana, string imagePath)
        {
            var card = new Card
            {
                Name = cardName,
                Attack = attack,
                Defense = defense,
                Speed = speed,
                Mana = mana,
                ImagePath = imagePath
            };

            await _context.Cards.InsertOneAsync(card);

            return card;
        }

        public async Task<bool> FindCardByName(string cardName)
        {
            var cardResult = await _context.Cards.Find(c => c.Name == cardName).FirstOrDefaultAsync();
            return cardResult != null;
        }
    }
}
