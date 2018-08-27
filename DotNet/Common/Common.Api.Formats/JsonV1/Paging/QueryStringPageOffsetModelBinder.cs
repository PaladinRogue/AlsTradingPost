using System;
using System.Threading.Tasks;
using Common.Api.Pagination.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Common.Api.Formats.JsonV1.Paging
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
