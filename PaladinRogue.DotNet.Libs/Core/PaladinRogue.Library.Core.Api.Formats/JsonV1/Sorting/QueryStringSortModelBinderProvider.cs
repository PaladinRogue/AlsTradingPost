using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using PaladinRogue.Library.Core.Api.Sorting;

namespace PaladinRogue.Library.Core.Api.Formats.JsonV1.Sorting
{
    public class QueryStringSortModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(ISortTemplate).GetProperty(nameof(ISortTemplate.Sort)).PropertyType)
            {
                return new BinderTypeModelBinder(typeof(QueryStringSortModelBinder));
            }

            return null;
        }
    }
}
