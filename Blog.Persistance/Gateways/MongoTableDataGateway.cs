using Blog.Persistance.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MongoDB.Driver;

namespace Blog.Persistance.Gateways
{
    public class MongoTableDataGateway<TEntity> : ITableDataGateway<TEntity>
    {
        private const string PrimaryField = "_id";
        private readonly IMongoCollection<TEntity> mongoCollection;
        public MongoTableDataGateway(IMongoCollection<TEntity> mongoCollection)
        {
            this.mongoCollection = mongoCollection;
        }

        public void Create(TEntity entity)
        {
            mongoCollection.InsertOne(entity);
        }

        public void DeleteMany(Expression<Func<TEntity, bool>> filter)
        {
            mongoCollection.DeleteMany(filter);
        }

        public void Delete(string id)
        {
            mongoCollection.DeleteOne(new BsonDocument(PrimaryField, new ObjectId(id)));
        }

        public IList<TEntity> Find(Expression<Func<TEntity, bool>> filter)
        {
            return mongoCollection.Find(filter).ToList();
        }

        public TEntity FindById(string id)
        {
            return mongoCollection.Find(new BsonDocument(PrimaryField, new ObjectId(id))).Single();
        }

        public TEntity Single(Expression<Func<TEntity, bool>> filter)
        {
            return mongoCollection.Find(filter).Single();
        }

        public void Update(string id, TEntity entity)
        {
            mongoCollection.ReplaceOne(new BsonDocument(PrimaryField, new ObjectId(id)), entity);
        }
    }
}
