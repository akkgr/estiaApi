
using estiaApi.Enumerations;
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

        [BsonElement("fathername")]
        public string FatherName { get; set; }

        [BsonElement("telephone")]
        public string Telephone { get; set; }

        [BsonElement("mobile")]
        public string Mobile { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("afm")]
        public string Afm { get; set; }

        [BsonElement("doy")]
        public string Doy { get; set; }

        [BsonElement("roleType")]
        [BsonRepresentation(BsonType.String)]
        public RoleType RoleType { get; set; }
    }
}