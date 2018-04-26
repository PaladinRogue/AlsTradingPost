using System;
using System.Collections.Generic;
using Common.Api.Builders.Resource;
using Common.Api.Links;
using Common.Api.Resources;

namespace Common.Api.Builders.Template
{
    public class TemplateBuilder : ITemplateBuilder
    {
        private ResourceBuilderResource<ITemplate> _template;
        
        private ITemplate _templateData;
        
        private readonly IBuildHelper _buildHelper;

        public TemplateBuilder(IBuildHelper buildHelper)
        {
            _buildHelper = buildHelper;
        }

        public ITemplateBuilder Create<T>() where T : ITemplate
        {
            _templateData = Activator.CreateInstance<T>();

            _template = _buildHelper.BuildResourceBuilder(_templateData);

            BuildHelper.AddSearchQueryParams(_template.Links, _templateData);

            return this;
        }

        public ITemplateBuilder WithTemplateMeta()
        {
            BuildHelper.BuildValidationMeta(_template.Meta, _templateData);

            return this;
        }

        public IDictionary<string, object> Build()
        {
            return BuildHelper.Build(_template);
        }
    }
}