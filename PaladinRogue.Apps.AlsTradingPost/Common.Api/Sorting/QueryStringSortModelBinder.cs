using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Resources.Sorting;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Common.Api.Sorting
{
    public class QueryStringSortModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            if (bindingContext.ModelType != typeof(ISortTemplate).GetProperty(nameof(ISortTemplate.Sort)).PropertyType)
            {
                return Task.CompletedTask;
            }

            ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.FieldName);

            List<SortBy> sort = new List<SortBy>();

            foreach (string sortTerm in valueProviderResult.Values)
            {
                sort.Add(new SortBy
                {
                    IsAscending = !sortTerm.StartsWith('-'),
                    PropertyName = sortTerm.Replace("-", string.Empty)
                });
            }

            bindingContext.Result = ModelBindingResult.Success(sort);

            return Task.CompletedTask;
        }
    }
}
