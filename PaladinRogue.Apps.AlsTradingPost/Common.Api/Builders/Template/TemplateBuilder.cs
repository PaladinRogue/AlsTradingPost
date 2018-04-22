using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Builders.Dictionary;

namespace Common.Api.Builders.Template
{
    public class TemplateBuilder<TTemplate> : ITemplateBuilder
    {
        private readonly TemplateBuilderTemplate<TTemplate> _template;

        private readonly TTemplate _templateData;

        private TemplateBuilder()
        {
            _templateData = Activator.CreateInstance<TTemplate>();

            _template = new TemplateBuilderTemplate<TTemplate>
            {
                Data = BuildHelper.BuildTemplateData(_templateData),
                Meta = BuildHelper.BuildMeta(_templateData),
                Links = BuildHelper.BuildLinks(_templateData)
            };
        }

        public static TemplateBuilder<TTemplate> Create()
        {
            return new TemplateBuilder<TTemplate>();
        }

        public ITemplateBuilder WithMeta()
        {
            BuildHelper.BuildValidationMeta(_template.Meta, _templateData);

            return this;
        }

        public IDictionary<string, object> Build()
        {
            return DictionaryBuilder<string, object>.Create()
                .Add(_template.Data.TypeName, DictionaryBuilder<string, object>.Create()
                    .Add(ResourceType.Data, _template.Data.Resource)
                    .Add(ResourceType.Meta, _template.Meta.Properties.BuildPropertyDictionary())
                    .Add(ResourceType.Links, _template.Links.BuildLinkDictionary())
                    .Build())
                .Build();
        }
    }
}