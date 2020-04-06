
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace estiaApi.Entities
{
    [BsonIgnoreExtraElements]
    public class Person
    {
        [BsonElement("lastname")]
        public string LastName { get; set; }
        [BsonElement("firstname")]
        public string FirstName { get; set; }
        [BsonElement("telephone")]
        public string Telephone { get; set; }
        [BsonElement("mobile")]
        public string Mobile { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
    }
}