
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Intertek.DataAccess.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        string ConnectionString { get; }
        IList<TEntity> GetAll();
        IList<TEntity> GetAll(params string[] includes);
        IList<TEntity> GetFiltered(Expression<Func<TEntity, bool>> whereCondition);
        IList<TEntity> GetFiltered(Expression<Func<TEntity, bool>> whereCondition, params string[] includes);
        IList<TEntity> GetPaged<TKey>(Expression<Func<TEntity, bool>> whereCondition, Expression<Func<TEntity, TKey>> orderBy, bool ascending, params string[] includes);
        IList<TEntity> GetPaged<TKey>(Expression<Func<TEntity, bool>> whereCondition, Expression<Func<TEntity, TKey>> orderBy, bool ascending, int startRow, int pageLength, out long totalCount, params string[] includes);
        TEntity Single(Expression<Func<TEntity, bool>> whereCondition);
        TEntity Single(Expression<Func<TEntity, bool>> whereCondition, params string[] includes);
        int Count();
        int Count(Expression<Func<TEntity, bool>> whereCondition);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        int Delete(TEntity entity);
        void  Attach<TQ>(TQ entity);
        void Detach<TQ>(TQ entity);
    }
}
