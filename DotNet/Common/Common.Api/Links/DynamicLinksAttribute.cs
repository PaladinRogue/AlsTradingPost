using System;

namespace Common.Api.Links
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DynamicLinksAttribute : Attribute
    {
        public Type DynamicLinksProviderType { get; }

        public DynamicLinksAttribute(Type dynamicLinksProviderType)
        {
            if (dynamicLinksProviderType == null || !typeof(IDynamicLinksProvider).IsAssignableFrom(dynamicLinksProviderType))
            {
                throw new ArgumentOutOfRangeException(nameof(dynamicLinksProviderType));
            }

            DynamicLinksProviderType = dynamicLinksProviderType;
        }
    }
}
