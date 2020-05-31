
using estiaApi.Enumerations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace estiaApi.Entities
{
    [BsonIgnoreExtraElements]
    public class Apartment : Entity
    {
        [BsonElement("buildingId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BuildingId { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("position")]
        public int Position { get; set; }

        [BsonElement("resident")]
        public Person Resident { get; set; }

        [BsonElement("owner")]
        public Person Owner { get; set; }

        [BsonElement("closed")]
        public bool Closed { get; set; }

        [BsonElement("infoType")]
        [BsonRepresentation(BsonType.String)]
        public InfoType InfoType { get; set; }

        [BsonElement("common")]
        public decimal Common { get; set; }

        [BsonElement("lift")]
        public decimal Lift { get; set; }

        [BsonElement("ei")]
        public decimal Ei { get; set; }

        [BsonElement("fi")]
        public decimal Fi { get; set; }

        [BsonElement("owners")]
        public decimal owners { get; set; }

        [BsonElement("special")]
        public decimal Special { get; set; }

        [BsonElement("heat")]
        public decimal Heat { get; set; }

        [BsonIgnore]
        public string BuildingTitle { get; set; }
    }
}