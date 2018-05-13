using System;
using System.Collections.Generic;
using Common.Api.Builders.Resource;
using Common.Api.Meta;
using Common.Api.Resources;

namespace Common.Api.Builders.Template
{
    public class TemplateBuilder : ITemplateBuilder
    {
        private ResourceBuilderResource<ITemplate> _template;
        
        private ITemplate _templateData;
        
        private readonly IBuildHelper _buildHelper;
        private readonly IMetaBuilder _metaBuilder;

        public TemplateBuilder(IBuildHelper buildHelper, IMetaBuilder metaBuilder)
        {
            _buildHelper = buildHelper;
            _metaBuilder = metaBuilder;
        }

        public ITemplateBuilder Create<T>() where T : ITemplate
        {
            _templateData = Activator.CreateInstance<T>();

            _template = _buildHelper.BuildResourceBuilder(_templateData);

            return this;
        }

        public ITemplateBuilder WithTemplateMeta()
        {
            _metaBuilder.BuildValidationMeta(_template.Meta, _templateData);

            return this;
        }

        public ITemplateBuilder WithSearching()
        {
            BuildHelper.AddSearchQueryParams(_template.Links, _templateData);

            return this;
        }

        public IDictionary<string, IBuiltResource> Build()
        {
            return BuildHelper.Build(_template);
        }
    }
}