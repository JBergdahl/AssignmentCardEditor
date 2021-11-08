using System.Collections.Generic;
using System.Linq;
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

        public async Task<Card> AddOneCard(string cardName, string cardType, int attack, int defense, int speed,
            int mana, string imagePath, string description)
        {
            var card = new Card
            {
                Name = cardName,
                CardType = cardType,
                Attack = attack,
                Defense = defense,
                Speed = speed,
                Mana = mana,
                ImagePath = imagePath,
                Description = description
            };

            await _context.Cards.InsertOneAsync(card);

            return card;
        }

        public async Task<CardType> AddOneCardType(string cardTypeName, int attackDefault, int defenseDefault,
            int speedDefault, int manaDefault)
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

        public async Task<bool> IsCardNameInDatabase(string cardName)
        {
            var cardResult = await _context.Cards.Find(c => c.Name.ToUpper() == cardName.ToUpper()).FirstOrDefaultAsync();
            return cardResult != null;
        }

        public async Task<bool> IsCardTypeNameInDatabase(string typeName)
        {
            var cardResult = await _context.CardTypes.Find(c => c.Name.ToUpper() == typeName.ToUpper()).FirstOrDefaultAsync();
            return cardResult != null;
        }

        public CardType GetCardTypeByName(string typeName)
        {
            var cardResult = _context.CardTypes.Find(c => c.Name == typeName).FirstOrDefault();
            return cardResult;
        }

        public List<CardType> GetAllCardTypes()
        {
            var cardTypes = _context.CardTypes.AsQueryable().ToList();
            return cardTypes;
        }

        public List<Card> GetAllCards()
        {
            return _context.Cards.AsQueryable().ToList();
        }

        public void DeleteOneCardByName(string name)
        {
            _context.Cards.DeleteOne(c => c.Name == name);
        }

        public void DeleteOneCardTypeByName(string name)
        {
            _context.CardTypes.DeleteOne(c => c.Name == name);
        }

        public Card GetCardByName(string cardName)
        {
            return _context.Cards.Find(c => c.Name == cardName).FirstOrDefault();
        }
    }
}