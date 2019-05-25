using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

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

        public static Expression<Func<TIn, TOut>> CreatePropertyAccessor<TIn, TOut>(this string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return null;
            }

            ParameterExpression param = Expression.Parameter(typeof(TIn));
            MemberExpression body = Expression.PropertyOrField(param, propertyName);
            return Expression.Lambda<Func<TIn, TOut>>(body, param);
        }
        
        public static string Format(this string str, IDictionary<string, string> parameters)  
        {
            StringBuilder sb = new StringBuilder(str);  
            foreach(KeyValuePair<string, string> kv in parameters)  
            {  
                sb.Replace($"{{{kv.Key}}}", kv.Value ?? "");  
            }  
  
            return sb.ToString();  
        }  
    }
}
