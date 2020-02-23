using Autofac;
using BBC.Core.Configuration;
using BBC.Core.Domain.Helper;
using BBC.Core.IoC;
using BBC.Core.Module;
using BBC.Core.Registery;
using BBC.Infrastructure.Data;
using BBC.Infrastructure.Registery;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BBC.Infrastructure
{
    public class InfrastructureModule : BaseModule
    {
        public override void PreInit(IServiceCollection services)
        {
            var scopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var config = provider.GetRequiredService<IConfiguration>();
                var connectionString = config.GetConnectionString("Default");

                services.AddDbContext<BBCContext>(opt =>
                {
                    opt.UseSqlServer(connectionString);
                });
            }
        }



        protected override void Load(ContainerBuilder builder)
        {
            builder.DIRegister();
            builder.RegisterDatabase();
        }

        public override void PostInit(ContainerBuilder builder)
        {
            RegisterEntities();
        }

        private void RegisterEntities()
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

        private List<PropertyInfo> GetDbSetProperties(DbContext context)
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
