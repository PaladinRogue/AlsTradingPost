using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using PaladinRogue.Libray.Core.Api.Resources;

namespace PaladinRogue.Libray.Core.Api.Formats.JsonV1.Filtering
{
    public class QueryStringFilterModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ContainerMetadata != null && context.Metadata.ContainerMetadata.ModelType.GetInterfaces().Contains(typeof(ISearchTemplate)))
            {
                return new BinderTypeModelBinder(typeof(QueryStringFilterModelBinder));
            }

            return null;
        }
    }
}
