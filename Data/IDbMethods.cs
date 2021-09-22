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
        Task<Card> AddOneCard(string cardName, int attack, int defense, int speed, int mana, string imagePath);
        Task<bool> FindCardByName(string cardName);
    }
}
