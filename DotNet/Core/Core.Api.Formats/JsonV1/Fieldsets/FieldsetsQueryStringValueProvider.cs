using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using PaladinRogue.Libray.Core.Api.Fieldsets;
using PaladinRogue.Libray.Core.Api.NamingMap;

namespace PaladinRogue.Libray.Core.Api.Formats.JsonV1.Fieldsets
{
    public class FieldsetsQueryStringValueProvider : IFieldsetsQueryStringValueProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INamingMapProvider _namingMapProvider;

        public FieldsetsQueryStringValueProvider(
            IHttpContextAccessor httpContextAccessor,
            INamingMapProvider namingMapProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _namingMapProvider = namingMapProvider;
        }

        public IEnumerable<string> GetFieldsForResourcetype(Type resourceType)
        {
            return _httpContextAccessor.HttpContext.Request.Query[$"fields[{ _namingMapProvider.GetForType(resourceType) }]"]
                .SelectMany(x => x.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

        }
    }
}
