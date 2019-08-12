using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PaladinRogue.Library.Core.Api.Pagination.Interfaces;

namespace PaladinRogue.Library.Core.Api.Formats.JsonV1.Paging
{
    public class QueryStringPageOffsetModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            if (bindingContext.ModelType != typeof(IPaginationTemplate).GetProperty(nameof(IPaginationTemplate.PageOffset)).PropertyType)
            {
                return Task.CompletedTask;
            }

            ValueProviderResult value = bindingContext.ValueProvider.GetValue(PaginationQueryParams.PageOffset);

            bindingContext.Result = ModelBindingResult.Success(int.Parse(value.FirstValue));

            return Task.CompletedTask;
        }
    }
}
