using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Orion.Core.Domain.Contracts;

namespace Orion.Manager.Infra.Data.Context
{
    public static class SoftDeleteExtensions
    {
        public static void ConfigureSoftDelete(this ModelBuilder builder)
        {
            void SetSoftDeleteFilter(Type type)
            {
                typeof(SoftDeleteExtensions)
                    .GetMethod(
                        "AddSoftDeleteFilter",
                        BindingFlags.NonPublic |
                        BindingFlags.Static)!
                    .MakeGenericMethod(type)
                    .Invoke(null, new object[] {builder});
            }
            
            builder.Model.GetEntityTypes()
                .Where(e => typeof(IAuditableEntity).IsAssignableFrom(e.ClrType))
                .ToList()
                .ForEach(e => SetSoftDeleteFilter(e.ClrType));
        }

        private static void AddSoftDeleteFilter<TEntity>(this ModelBuilder builder)
            where TEntity : class, IAuditableEntity =>
            builder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);
        
        public static void ConfigureMapping(this ModelBuilder builder, Type type) => 
            builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(type)!);
    }
}