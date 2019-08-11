using Microsoft.AspNetCore.Mvc.Filters;

namespace PaladinRogue.Libray.Core.Api.QueryString
{
    public class SeparatedQueryStringFilter : IResourceFilter
    {
        private readonly SeparatedQueryStringValueProviderFactory _factory;

        public SeparatedQueryStringFilter(string key, string separator)
        {
            _factory = SeparatedQueryStringValueProviderFactory.Create(key, separator);
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.ValueProviderFactories.Insert(0, _factory);
        }
    }
}
