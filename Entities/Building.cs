
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace estiaApi.Entities
{
    [BsonIgnoreExtraElements]
    public class Building : Entity
    {
        [BsonElement("address")]
        public Address Address { get; set; }
    }
}