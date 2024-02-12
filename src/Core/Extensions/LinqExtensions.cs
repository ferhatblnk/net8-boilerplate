using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Extensions
{
    public static class LinqExtensions
    {
        private static PropertyInfo GetPropertyInfo(Type objType, string name)
        {
            var properties = objType.GetProperties();
            return properties.FirstOrDefault(p => p.Name == name);
        }
        private static LambdaExpression GetExpression(Type objType, PropertyInfo pi)
        {
            var paramExpr = Expression.Parameter(objType);
            var propAccess = Expression.PropertyOrField(paramExpr, pi.Name);
            var expr = Expression.Lambda(propAccess, paramExpr);
            return expr;
        }

        private static IQueryable<T> Filter<T, V>(this IQueryable<T> query, string propertyName, V propertyValue)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T));
                var left = Expression.Property(parameter, propertyName);
                Expression<Func<object>> right = () => propertyValue;
                var convertedRight = Expression.Convert(right.Body, propertyValue.GetType());
                var body = Expression.Call(left, nameof(string.Contains), Type.EmptyTypes, convertedRight);
                var predicate = Expression.Lambda<Func<T, bool>>(body, new ParameterExpression[] { parameter });

                return query.Where(predicate);
            }
            catch
            {
                return query;
            }
        }

        private static IQueryable<T> OrderByQueryable<T>(IQueryable<T> query, string name, bool asc = true)
        {
            var propInfo = GetPropertyInfo(typeof(T), name);

            if (propInfo == null)
                return query;

            var expr = GetExpression(typeof(T), propInfo);
            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == (asc ? "OrderBy" : "OrderByDescending") && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);

            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
        }

        private static IEnumerable<T> OrderByEnumerable<T>(IEnumerable<T> query, string name, bool asc = true)
        {
            var propInfo = GetPropertyInfo(typeof(T), name);

            if (propInfo == null)
                return query;

            var expr = GetExpression(typeof(T), propInfo);
            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == (asc ? "OrderBy" : "OrderByDescending") && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);

            return (IEnumerable<T>)genericMethod.Invoke(null, new object[] { query, expr.Compile() });
        }

        public static IQueryable<T> ContainsGeneric<T>(this IQueryable<T> query, string name, string value)
        {
            return Filter(query, name, value);
        }

        public static IEnumerable<T> OrderByGeneric<T>(this IEnumerable<T> query, string name)
        {
            return OrderByEnumerable(query, name);
        }

        public static IQueryable<T> OrderByGeneric<T>(this IQueryable<T> query, string name)
        {
            return OrderByQueryable(query, name);
        }

        public static IEnumerable<T> OrderByDescendingGeneric<T>(this IEnumerable<T> query, string name)
        {
            return OrderByEnumerable(query, name, asc: false);
        }

        public static IQueryable<T> OrderByDescendingGeneric<T>(this IQueryable<T> query, string name)
        {

            return OrderByQueryable(query, name, asc: false);
        }
    }
}
