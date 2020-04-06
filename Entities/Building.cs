
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace estiaApi.Entities
{
    [BsonIgnoreExtraElements]
    public class Building
    {
        public Building()
        {
            Appartments = new List<Appartment>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("address")]
        public Address Address { get; set; }

        [BsonElement("appartments")]
        public List<Appartment> Appartments { get; set; }
    }
}