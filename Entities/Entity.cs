
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace estiaApi.Entities
{
    [BsonIgnoreExtraElements]
    public abstract class Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("createdOn")]
        public DateTime CreatedOn { get; set; }

        [BsonElement("createdBy")]
        public string CreatedBy { get; set; }

        [BsonElement("updatedOn")]
        public DateTime UpdatedOn { get; set; }

        [BsonElement("updatedBy")]
        public string UpdatedBy { get; set; }

        [BsonElement("deleted")]
        public bool Deleted { get; set; }

        [BsonElement("deletedOn")]
        public DateTime DeletedOn { get; set; }

        [BsonElement("deletedBy")]
        public string DeletedBy { get; set; }
    }

}