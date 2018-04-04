using System;
using System.Linq.Expressions;
using EfCoreTest.Data.Models;
using EfCoreTest.Data.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreTest.Data.Mapping
{
    public abstract class BaseEntityTypeConfiguration<TEntity>: IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity
    {
        private readonly IDateTimeProvider dateTimeProvider;

        protected BaseEntityTypeConfiguration(IDateTimeProvider dateTimeProvider)
        {
            this.dateTimeProvider = dateTimeProvider;
        }

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            Expression body = Expression.Constant(true);
            Expression dateTimeUtcNow = Expression.Property(Expression.Constant(dateTimeProvider), "UtcNow");

            if (typeof(ISoftDeletable).IsAssignableFrom(typeof(TEntity)))
            {
                // ReSharper disable once SuspiciousTypeConversion.Global
                builder.Property(x => (x as ISoftDeletable).DeletedOn).IsRequired(false);

                body = Expression.AndAlso(body, 
                    Expression.OrElse(
                        Expression.GreaterThan(Expression.Property(parameter, "DeletedOn"), Expression.Convert(dateTimeUtcNow, typeof(DateTime?))),
                        Expression.Equal(Expression.Property(parameter, "DeletedOn"), Expression.Constant(null))
                    )
                );
            }
            
            var finalExpression = Expression.Lambda(body, parameter);
            builder.HasQueryFilter(finalExpression);
        }
    }
}