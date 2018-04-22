using System;
using System.Linq.Expressions;

namespace Common.Resources.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str)
        {
           return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
        }

        public static Expression<Func<TIn, object>> CreatePropertyAccessor<TIn>(this string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return null;
            }

            ParameterExpression param = Expression.Parameter(typeof(TIn));
            MemberExpression body = Expression.PropertyOrField(param, propertyName);
            return Expression.Lambda<Func<TIn, object>>(body, param);
        }
    }
}
