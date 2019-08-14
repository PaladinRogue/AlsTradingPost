using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PaladinRogue.Library.Core.Api.QueryString
{
    public class SeparatedQueryStringValueProviderFactory : IValueProviderFactory
    {
        private readonly string _separator;
        private readonly string _key;

        private SeparatedQueryStringValueProviderFactory(string key, string separator)
        {
            _key = key;
            _separator = separator;
        }

        public static SeparatedQueryStringValueProviderFactory Create(string separator)
        {
            return new SeparatedQueryStringValueProviderFactory(null, separator);
        }

        public static SeparatedQueryStringValueProviderFactory Create(string key, string separator)
        {
            return new SeparatedQueryStringValueProviderFactory(key, separator);
        }

        public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            context.ValueProviders.Insert(0, new SeparatedQueryStringValueProvider(_key, context.ActionContext.HttpContext.Request.Query, _separator));
            return Task.CompletedTask;
        }
    }
}
