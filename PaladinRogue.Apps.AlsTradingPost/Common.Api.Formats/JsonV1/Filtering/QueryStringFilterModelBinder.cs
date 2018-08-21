using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Common.Api.Formats.JsonV1.Filtering
{
    public class QueryStringFilterModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            ValueProviderResult value = bindingContext.ValueProvider.GetValue($"filter[{ bindingContext.FieldName }]");
            
            bindingContext.Result = ModelBindingResult.Success(value.FirstValue);

            return Task.CompletedTask;
        }
    }
}
