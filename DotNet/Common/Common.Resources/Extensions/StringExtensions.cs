using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Common.Resources.Extensions
{
    public static class String
    {
        private static readonly Random _random = new Random();

        public static string Random(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public static string RandomSpecial(int length)
        {
            const string chars = "!£$%^&*()_+-=[]{}'#@~,.<>?";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public static string RandomChar(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public static string RandomNumeric(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }

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

        public static string Format<T>(this string str, IDictionary<string, T> parameters)
        {
            return parameters
                .Aggregate(str, (current, kv) => current.Replace($"{{{kv.Key}}}", kv.Value?.ToString() ?? "", StringComparison.OrdinalIgnoreCase));
        }
    }
}
