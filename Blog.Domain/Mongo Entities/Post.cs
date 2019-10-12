using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Blog.Domain.MongoEntities
{
    public class Post : IPost
    {
        [BsonIgnoreIfDefault(true)]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageFileName { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime EditedTimestamp { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string EditorId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string AuthorId { get; set; }

        [BsonIgnore]
        public IList<ILike> LikesCollection { get; set; } = new List<ILike>();
        
        [BsonIgnore]
        public IList<IComment> CommentsCollection { get; set; } = new List<IComment>();
    }
}
