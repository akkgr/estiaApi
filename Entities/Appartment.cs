
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace estiaApi.Entities
{
    [BsonIgnoreExtraElements]
    public class Appartment
    {
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("position")]
        public int Position { get; set; }
        [BsonElement("resident")]
        public Person Resident { get; set; }
        [BsonElement("owner")]
        public Person Owner { get; set; }
    }
}