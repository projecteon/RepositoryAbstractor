using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq.Expressions;
using System.Reflection;

namespace Repository.EntityFramework
{
	public class PrivateEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : class, IPersistable
    {
        public static ManyNavigationPropertyConfiguration<T, U> HasMany<T, U>(EntityTypeConfiguration<T> mapper, String fieldName)
            where T : class
            where U : class
        {
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            PropertyInfo pi = type.GetProperty(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            expr = Expression.Property(expr, pi);
            LambdaExpression lambda = Expression.Lambda(expr, arg);
            var expression = (Expression<Func<T, ICollection<U>>>)lambda;
            return mapper.HasMany(expression);
        }

        public static StringPropertyConfiguration PropertyStr<T>(EntityTypeConfiguration<T> mapper, String propertyName) where T : class
        {
            Type type = typeof (T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            PropertyInfo pi = type.GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            expr = Expression.Property(expr, pi);
            LambdaExpression lambda = Expression.Lambda(expr, arg);
            var expression = (Expression<Func<T, string>>) lambda;
            return mapper.Property(expression);
        }
    }
}
