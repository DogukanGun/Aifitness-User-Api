using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Aifitness_User_Api.Data.Abstraction
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }

        DateTime CreatedAt { get; }

        string CreatedBy { get; set; }
    }
}
