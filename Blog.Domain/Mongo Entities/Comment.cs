using System;
using System.Collections.Generic;
using System.Text;
using Blog.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Blog.Domain.MongoEntities
{
    public class Comment : IComment
    {
        [BsonIgnoreIfDefault(true)]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Content { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string AuthorId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string PostId { get; set; }

        [BsonIgnore]
        public IList<ILike> LikesCollection { get; set; } = new List<ILike>();
    }
}
