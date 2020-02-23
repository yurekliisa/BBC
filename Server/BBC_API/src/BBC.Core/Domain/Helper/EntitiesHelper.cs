using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBC.Core.Domain.Helper
{
    public static class EntitiesHelper
    {
        private static Dictionary<Type, List<Type>> EntityBaseList = new Dictionary<Type, List<Type>>();

        public static void SetEntityBases(Type entity)
        {
            var entityBases = entity.GetInterfaces().ToList();
            if (entityBases.Count > 0)
            {
                EntityBaseList.Add(entity, entityBases);
            }
        }

        public static List<Type> GetEntityBases(Type entity)
        {
            KeyValuePair<Type, List<Type>> result = EntityBaseList.FirstOrDefault(x => x.Key == entity);

            if (result.Value == null) return null;

            return result.Value;
        }

        public static bool ExistsEntity(Type entity)
        {
            return EntityBaseList.ContainsKey(entity);
        }

        public static bool ExistsEntityWithBaseEntity(Type entity, Type baseEntity)
        {
            if (ExistsEntity(entity))
            {
                var entityBases = GetEntityBases(entity);

                if (entityBases == null) return false;

                return entityBases.Contains(baseEntity);
            }

            return false;
        }


    }
}
