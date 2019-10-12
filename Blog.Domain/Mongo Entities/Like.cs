using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using Blog.Domain.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Collections;

namespace Blog.Domain.MongoEntities
{
    public class Like : ILike
    {
        [BsonIgnoreIfDefault(true)]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string LikedElement { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string OwnerId { get; set; }
    }
}
