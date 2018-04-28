using System;
using Common.Api.Resources;

namespace Common.Api.Meta
{
    public interface IMetaBuilder
    {
        Meta BuildMeta<T>(T data);
        void BuildValidationMeta<T>(Meta meta, T data);
        void BuildFieldMeta<T>(Meta meta, T data);
        void BuildSortingMeta<T>(Meta meta, T data, Type summaryResourceType) where T : ITemplate;
    }
}