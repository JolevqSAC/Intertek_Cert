
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.EntityClient;
using System.Data.Objects.DataClasses;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Intertek.Business.Entities;
using Intertek.Helpers;

namespace Intertek.DataAccess.Repository
{
    public abstract class Repository<TData, TEntity> : Singleton<TData>, IRepository<TEntity>
        where TData : class, new()
        where TEntity : EntityObject
    {
        #region Atributos

        public static ANALISIS_ENSAYOEntities Context;

        #endregion

        #region Propiedades

        public string ConnectionString
        {
            get
            {
                var entityConnection = (EntityConnection)DataContext.Connection;
                var sqlConnection = (SqlConnection)entityConnection.StoreConnection;
                return sqlConnection.ConnectionString;
            }
        }

        public ANALISIS_ENSAYOEntities DataContext
        {
            get { return Context ?? (Context = new ANALISIS_ENSAYOEntities()); }
        }

        #endregion

        #region Miembros de IRepository<TEntity>

        public IList<TEntity> GetAll()
        {
            var entities = DataContext.CreateQuery<TEntity>("[" + typeof(TEntity).Name + "]").ToList();
            Dispose();
            return entities;
        }

        public IList<TEntity> GetAll(params string[] includes)
        {
            var entities = DataContext.CreateQuery<TEntity>(includes).ToList();
            Dispose();
            return entities;
        }

        public IList<TEntity> GetFiltered(Expression<Func<TEntity, bool>> whereCondition)
        {
            var entities = DataContext.CreateQuery<TEntity>("[" + DataContext.GetEntitySet<TEntity>().Name + "]").Where(whereCondition).ToList();
            Dispose();
            return entities;
        }

        public IList<TEntity> GetFiltered(Expression<Func<TEntity, bool>> whereCondition, params string[] includes)
        {
            var entities = DataContext.CreateQuery<TEntity>(includes).Where(whereCondition).ToList();
            Dispose();
            return entities;
        }

        public IList<TEntity> GetFiltered(string sidx, string sord, int rows, int start, string where, params string[] includes)
        {
            var entities = string.IsNullOrWhiteSpace(where)
                            ? DataContext.CreateQuery<TEntity>(includes).OrderBy(sidx + " " + sord).Skip(start).Take(rows).ToList()
                            : DataContext.CreateQuery<TEntity>(includes).Where(where).OrderBy(sidx + " " + sord).Skip(start).Take(rows).ToList();
            Dispose();
            return entities;
        }

        public IList<TEntity> GetPaged<TKey>(Expression<Func<TEntity, bool>> whereCondition, Expression<Func<TEntity, TKey>> orderBy, bool ascending, params string[] includes)
        {
            var entities = ascending
                            ? DataContext.CreateQuery<TEntity>(includes).Where(whereCondition).OrderBy(orderBy).ToList()
                            : DataContext.CreateQuery<TEntity>(includes).Where(whereCondition).OrderByDescending(orderBy).ToList();
            Dispose();
            return entities;
        }

        public IList<TEntity> GetPaged<TKey>(Expression<Func<TEntity, bool>> whereCondition, Expression<Func<TEntity, TKey>> orderBy, bool ascending, int startRow, int pageLength, out long totalCount, params string[] includes)
        {
            var query = ascending
                            ? DataContext.CreateQuery<TEntity>(includes).Where(whereCondition).OrderBy(orderBy)
                            : DataContext.CreateQuery<TEntity>(includes).Where(whereCondition).OrderByDescending(orderBy);
            totalCount = query.Count();
            var entities = query.Skip(startRow).Take(pageLength).ToList();
            Dispose();
            return entities;
        }

        public TEntity Single(Expression<Func<TEntity, bool>> whereCondition)
        {
            var entity = DataContext.CreateQuery<TEntity>("[" + DataContext.GetEntitySet<TEntity>().Name + "]").FirstOrDefault(whereCondition);
            Dispose();
            return entity;
        }

        public TEntity Single(Expression<Func<TEntity, bool>> whereCondition, params string[] includes)
        {
            var entity = DataContext.CreateQuery<TEntity>(includes).Where(whereCondition).FirstOrDefault();
            Dispose();
            return entity;
        }

        public int Count()
        {
            var count = DataContext.CreateQuery<TEntity>("[" + DataContext.GetEntitySet<TEntity>().Name + "]").Count();
            Dispose();
            return count;
        }

        public int Count(Expression<Func<TEntity, bool>> whereCondition)
        {
            var count = DataContext.CreateQuery<TEntity>("[" + DataContext.GetEntitySet<TEntity>().Name + "]").Count(whereCondition);
            Dispose();
            return count;
        }

        public virtual TEntity Add(TEntity entity)
        {
            try
            {
                var context = DataContext;
                var key = entity.EntityKey ?? context.CreateEntityKey(DataContext.GetEntitySet<TEntity>().Name, entity);
                //var key = entity.EntityKey ?? context.CreateEntityKey(entity.GetType().Name, entity);

                object originalItem;
                if (context.TryGetObjectByKey(key, out originalItem))
                {
                    if (originalItem is EntityObject && ((EntityObject)originalItem).EntityState != EntityState.Added)
                    {
                        context.ApplyPropertyChanges(key.EntitySetName, entity);
                    }
                }
                else
                {
                    context.AddObject(context.GetEntitySet<TEntity>().Name, entity);
                }
                context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Dispose();
            }
        }

        public virtual TEntity Update(TEntity entity)
        {
            try
            {
                Object originalItem;
                var context = DataContext;
                var key = context.CreateEntityKey(entity.EntityKey.EntitySetName, entity);

                if (context.TryGetObjectByKey(key, out originalItem))
                {
                    context.ApplyPropertyChanges(key.EntitySetName, entity);
                    foreach (var entityrelationship in ((IEntityWithRelationships)originalItem).RelationshipManager.GetAllRelatedEnds())
                    {
                        var oldRef = entityrelationship as EntityReference;
                        if (oldRef != null)
                        {
                            var newRef = ((IEntityWithRelationships)entity).RelationshipManager.GetRelatedEnd(oldRef.RelationshipName, oldRef.TargetRoleName) as EntityReference;
                            oldRef.EntityKey = newRef.EntityKey;
                        }
                    }
                    context.SaveChanges();
                }
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Dispose();
            }
        }

        public virtual int Delete(TEntity entity)
        {
            try
            {
                object originalItem;
                var context = DataContext;
                var key = context.CreateEntityKey(entity.EntityKey.EntitySetName, entity);
                if (context.TryGetObjectByKey(key, out originalItem))
                {
                    context.DeleteObject(originalItem);
                }
                var returnValue = context.SaveChanges();
                return returnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Dispose();
            }
        }

        public virtual void Attach<TQ>(TQ entity)
        {
            DataContext.AttachTo(DataContext.GetEntitySet<TQ>().Name, entity);
        }

        public virtual void Detach<TQ>(TQ entity)
        {
            DataContext.Detach(entity);
        }

        #endregion

        #region Miembros de IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (Context == null) return;
            Context.Dispose();
            Context = null;
        }

        #endregion
    }
}
