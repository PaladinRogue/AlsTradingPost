using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using PaladinRogue.Library.Core.Api.Pagination.Interfaces;

namespace PaladinRogue.Library.Core.Api.Formats.JsonV1.Paging
{
    public class QueryStringPageSizeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            string pageSizePropertyName = nameof(IPaginationTemplate.PageSize);

            if (context.Metadata.ModelType == typeof(IPaginationTemplate).GetProperty(pageSizePropertyName).PropertyType && context.Metadata.PropertyName == pageSizePropertyName)
            {
                return new BinderTypeModelBinder(typeof(QueryStringPageSizeModelBinder));
            }

            return null;
        }
    }
}
