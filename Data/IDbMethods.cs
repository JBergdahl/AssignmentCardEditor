using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardEditor.Domain;

namespace Data
{
    public interface IDbMethods
    {
        Task<Card> AddOneCard(string cardName, string cardType, int attack, int defense, int speed, int mana, string imagePath);
        Task<CardType> AddOneCardType(string cardTypeName, int attackDefault, int defenseDefault, int speedDefault, int manaDefault);
        Task<bool> IsCardNameInDatabase(string cardName);
        Task<bool> IsCardTypeNameInDatabase(string typeName);
        public CardType GetCardTypeByName(string typeName);
        List<CardType> GetAllCardTypes();
        List<Card> GetAllCards();
        void DeleteOneCardById(string id);
        Card GetCardByName(string cardName);
    }
}
