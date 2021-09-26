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

        public async Task<Card> AddOneCard(string cardName, string cardType, int attack, int defense, int speed, int mana, string imagePath)
        {
            var card = new Card
            {
                Name = cardName,
                CardType = cardType,
                Attack = attack,
                Defense = defense,
                Speed = speed,
                Mana = mana,
                ImagePath = imagePath
            };

            await _context.Cards.InsertOneAsync(card);

            return card;
        }

        public async Task<CardType> AddOneCardType(string cardTypeName, int attackDefault, int defenseDefault, int speedDefault, int manaDefault)
        {
            var cardType = new CardType
            {
                Name = cardTypeName,
                AttackDefault = attackDefault,
                DefenseDefault = defenseDefault,
                SpeedDefault = speedDefault,
                ManaDefault = manaDefault
            };

            await _context.CardTypes.InsertOneAsync(cardType);

            return cardType;
        }

        public async Task<bool> FindCardByName(string cardName)
        {
            var cardResult = await _context.Cards.Find(c => c.Name == cardName).FirstOrDefaultAsync();
            return cardResult != null;
        }

        public async Task<bool> FindCardTypeByName(string typeName)
        {
            var cardResult = await _context.CardTypes.Find(c => c.Name == typeName).FirstOrDefaultAsync();
            return cardResult != null;
        }

        public CardType FindCardTypeByNameNormal(string typeName)
        {
            var cardResult = _context.CardTypes.Find(c => c.Name == typeName).FirstOrDefault();
            return cardResult;
        }

        public List<CardType> GetAllCardTypes()
        {
            return _context.CardTypes.AsQueryable().ToList();
        }

        public List<Card> GetAllCards()
        {
            return _context.Cards.AsQueryable().ToList();
        }

        public void DeleteOneCard(string name)
        {
            _context.Cards.DeleteOne(c => c.Name == name);
        }

        public Card GetCardByName(string cardName)
        {
            return _context.Cards.Find(c => c.Name == cardName).FirstOrDefault();
        }
    }
}
