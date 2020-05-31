
using System;
using System.Collections.Generic;
using estiaApi.Enumerations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace estiaApi.Entities
{
    [BsonIgnoreExtraElements]
    public class Building : Entity
    {
        public Building()
        {
            Providers = new List<Provider>();
        }

        [BsonElement("address")]
        public Address Address { get; set; }

        [BsonElement("active")]
        public bool Active { get; set; }

        [BsonElement("management")]
        public bool Management { get; set; }

        [BsonElement("managementStart")]
        public DateTime ManagementStart { get; set; }

        [BsonElement("managementEnd")]
        public DateTime ManagementEnd { get; set; }

        [BsonElement("reserve")]
        public bool Reserve { get; set; }

        [BsonElement("heatingType")]
        [BsonRepresentation(BsonType.String)]
        public HeatingType HeatingType { get; set; }

        [BsonElement("calories")]
        public bool CaloriesCounter { get; set; }

        [BsonElement("closedApartmentParticipation")]
        public bool ClosedApartmentParticipation { get; set; }

        [BsonElement("litersPerCm")]
        public decimal LitersPerCm { get; set; }

        [BsonElement("bankReason")]
        public string BankReason { get; set; }

        [BsonElement("providers")]
        public List<Provider> Providers { get; set; }
    }
}