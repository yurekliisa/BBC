using Autofac;
using BBC.Core.Domain.Helper;
using BBC.Core.IoC;
using BBC.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BBC.Infrastructure.Registery
{
    public static class Entity
    {
        public static void RegisterEntities(this IApplicationBuilder app)
        {
            var context = IoCManager.Container.Resolve<BBCContext>();
            List<PropertyInfo> dbSetProperties = GetDbSetProperties(context);
            List<object> dbSets = dbSetProperties.Select(x => x.GetValue(context, null)).ToList();
            foreach (var model in dbSets)
            {
                if (EntitiesHelper.ExistsEntity(model.GetType()))
                {
                    EntitiesHelper.SetEntityBases(model.GetType());
                }
            }

        }

        private static List<PropertyInfo> GetDbSetProperties(DbContext context)
        {
            var dbSetProperties = new List<PropertyInfo>();
            var properties = context.GetType().GetProperties();

            foreach (var property in properties)
            {
                var setType = property.PropertyType;

                var isDbSet = setType.IsGenericType && (typeof(DbSet<>).IsAssignableFrom(setType.GetGenericTypeDefinition()));

                if (isDbSet)
                {
                    dbSetProperties.Add(property);
                }
            }

            return dbSetProperties;

        }

    }
}
