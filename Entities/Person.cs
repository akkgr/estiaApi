
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace estiaApi.Entities
{
    public class Person
    {
        [BsonElement("lastName")]
        public string LastName { get; set; }
        [BsonElement("firstName")]
        public int FirstName { get; set; }
        [BsonElement("telephone")]
        public int Telephone { get; set; }
        [BsonElement("mobile")]
        public int Mobile { get; set; }
        [BsonElement("email")]
        public int Email { get; set; }
    }
}