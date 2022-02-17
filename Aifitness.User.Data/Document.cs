
using Aifitness_User_Api.Data.Abstraction;
using MongoDB.Bson;
using System;

namespace Aifitness_User_Api.Data
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }
        public DateTime CreatedAt { get => Id.CreationTime; }
        public string CreatedBy { get; set; }
    }
}
