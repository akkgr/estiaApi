
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace estiaApi.Entities
{
    public class Appartment
    {
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("floor")]
        public int Floor { get; set; }
        [BsonElement("resident")]
        public Person Resident { get; set; }
        [BsonElement("owner")]
        public Person Owner { get; set; }
    }
}