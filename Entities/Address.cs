
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace estiaApi.Entities
{
    public class Address
    {
        [BsonElement("area")]
        public string Area { get; set; }
        [BsonElement("street")]
        public string Street { get; set; }
        [BsonElement("streetNumber")]
        public string StreetNumber { get; set; }
        [BsonElement("postalCode")]
        public string PostalCode { get; set; }
        [BsonElement("country")]
        public string Country { get; set; }
        [BsonElement("lat")]
        public double Lat { get; set; }
        [BsonElement("lng")]
        public double Lng { get; set; }
    }
}