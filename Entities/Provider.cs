
using estiaApi.Enumerations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace estiaApi.Entities
{
    public class Provider
    {
        [BsonElement("providerName")]
        [BsonRepresentation(BsonType.String)]
        public ProviderType ProviderType { get; set; }

        [BsonElement("providerName")]
        public string ProviderName { get; set; }

        [BsonElement("customerName")]
        public string CustomerName { get; set; }

        [BsonElement("contractNumber")]
        public string ContractNumber { get; set; }

        [BsonElement("connectionNumber")]
        public string ConnectionNumber { get; set; }

        [BsonElement("counterNumber")]
        public string CounterNumber { get; set; }

        [BsonElement("paymentCode")]
        public string PaymentCode { get; set; }

        [BsonElement("interval")]
        public int Interval { get; set; }

        [BsonElement("day")]
        public int Day { get; set; }

        [BsonElement("office")]
        public bool Office { get; set; }
    }
}