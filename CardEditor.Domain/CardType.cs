using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CardEditor.Domain
{
    public class CardType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("attack_default")]
        public int AttackDefault { get; set; }

        [BsonElement("defense_default")]
        public int DefenseDefault { get; set; }

        [BsonElement("speed_default")]
        public int SpeedDefault { get; set; }

        [BsonElement("mana_default")]
        public int ManaDefault { get; set; }
    }
}
