using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MongoDB.Bson;

namespace Blog.Persistance.Interfaces
{
    public interface ITableDataGateway<TEntity>
    {
        IList<TEntity> Find(Expression<Func<TEntity, bool>> filter);
        TEntity Single(Expression<Func<TEntity, bool>> filter);
        TEntity FindById(string id);
        void Create(TEntity entity);
        void Update(string id, TEntity entity);
        void Delete(string id);
        void DeleteMany(Expression<Func<TEntity, bool>> filter);
    }
}
