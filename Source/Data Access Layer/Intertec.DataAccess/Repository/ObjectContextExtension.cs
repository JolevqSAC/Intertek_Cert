
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;

namespace Intertek.DataAccess.Repository
{
    public static class ObjectContextExtensions
    {
        internal static EntitySetBase GetEntitySet<TEntity>(this ObjectContext context)
        {
            var container = context.MetadataWorkspace.GetEntityContainer(context.DefaultContainerName, DataSpace.CSpace);
            var baseType = GetBaseType(typeof(TEntity));
            return container.BaseEntitySets.Where(item => item.ElementType.Name.Equals(baseType.Name)).FirstOrDefault(); 
        }

        internal static ObjectQuery<TEntity> CreateQuery<TEntity>(this ObjectContext context, IEnumerable<string> includes)
        {
            Type baseType;
            var query = HasBaseType(typeof(TEntity), out baseType) ? context.CreateQuery<TEntity>("[" + baseType.Name + "]").OfType<TEntity>() : context.CreateQuery<TEntity>("[" + typeof(TEntity).Name + "]");

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }

        internal static void AttachUpdated(this ObjectContext context, EntityObject objectDetached)
        {
            if (objectDetached.EntityState == EntityState.Detached)
            {
                object original = null;
                if (context.TryGetObjectByKey(objectDetached.EntityKey, out original))
                    context.ApplyPropertyChanges(objectDetached.EntityKey.EntitySetName, objectDetached);
                else throw new ObjectNotFoundException();
            }
        }

        private static bool HasBaseType(Type type, out Type baseType)
        {
            var originalType = type.GetType();
            baseType = GetBaseType(type);
            return baseType != originalType;
        }

        private static Type GetBaseType(Type type)
        {
            var baseType = type.BaseType;
            return baseType != null && baseType != typeof(EntityObject) ? GetBaseType(type.BaseType) : type;
        }
    }
}
