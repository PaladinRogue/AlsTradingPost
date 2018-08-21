using System;
using Common.Api.Pagination.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Common.Api.Formats.JsonV1.Paging
{
    public class QueryStringPageOffsetModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            string pageSizePropertyName = nameof(IPaginationTemplate.PageOffset);

            if (context.Metadata.ModelType == typeof(IPaginationTemplate).GetProperty(pageSizePropertyName).PropertyType && context.Metadata.PropertyName == pageSizePropertyName)
            {
                return new BinderTypeModelBinder(typeof(QueryStringPageOffsetModelBinder));
            }

            return null;
        }
    }
}
