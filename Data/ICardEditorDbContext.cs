using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardEditor.Domain;
using MongoDB.Driver;

namespace Data
{
    public interface ICardEditorDbContext
    {
        IMongoCollection<Card> Cards { get; }
        IMongoCollection<CardType> CardTypes { get; }
    }
}
