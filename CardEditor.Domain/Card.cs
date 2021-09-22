using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CardEditor.Domain
{
    public class Card
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("attack")]
        public int Attack { get; set; }

        [BsonElement("defense")]
        public int Defense { get; set; }

        [BsonElement("speed")]
        public int Speed { get; set; }

        [BsonElement("mana")]
        public int Mana { get; set; }

        [BsonElement("image_path")]
        public string ImagePath { get; set; }
    }
}
