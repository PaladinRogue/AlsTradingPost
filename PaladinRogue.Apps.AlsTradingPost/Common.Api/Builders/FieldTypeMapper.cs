using System;
using System.Collections.Generic;

namespace Common.Api.Builders
{
    public static class FieldTypeMapper
    {
        private static readonly IDictionary<Type, string> FieldTypeMap = new Dictionary<Type, string>
        {
            { typeof(int), FieldType.Number },
            { typeof(bool), FieldType.Boolean },
            { typeof(string), FieldType.String }
        };

        public static bool HasFieldType<T>()
        {
            return HasFieldType(typeof(T));
        }

        public static bool HasFieldType(Type type)
        {
            return FieldTypeMap.ContainsKey(type);
        }

        public static string GetFieldType<T>()
        {
            return GetFieldType(typeof(T));
        }
        
        public static string GetFieldType(Type type)
        {
            if (FieldTypeMap.ContainsKey(type))
            {
                return FieldTypeMap[type];
            }
            
            throw new ArgumentException($"Field type mapping is not supported for { type.Name }");
        }
    }
}