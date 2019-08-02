using System;
using System.Collections.Generic;
using Common.Resources.Sorting;

namespace Common.Api.Formats.JsonV1.Formats
{
    public static class FieldTypeMapper
    {
        private static readonly IReadOnlyDictionary<Type, string> FieldTypeMap = new Dictionary<Type, string>
        {
            [typeof(int)]            = FieldType.Number,
            [typeof(int?)]           = FieldType.Number,
            [typeof(bool)]           = FieldType.Boolean,
            [typeof(bool?)]          = FieldType.Boolean,
            [typeof(string)]         = FieldType.String,
            [typeof(Guid)]           = FieldType.Id,
            [typeof(Guid?)]          = FieldType.Id,
            [typeof(IList<SortBy>)]  = FieldType.String
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
            if (HasFieldType(type))
            {
                return FieldTypeMap[type];
            }

            throw new ArgumentException($"Field type mapping is not supported for { type.Name }");
        }
    }
}