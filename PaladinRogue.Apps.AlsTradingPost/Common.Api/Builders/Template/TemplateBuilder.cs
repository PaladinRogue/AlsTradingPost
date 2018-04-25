using System;
using System.Collections.Generic;
using Common.Api.Builders.Resource;
using Common.Api.Links;

namespace Common.Api.Builders.Template
{
    public class TemplateBuilder<TTemplate> : ITemplateBuilder<TTemplate>
    {
        private ResourceBuilderResource<TTemplate> _template;
        
        private TTemplate _templateData;
        
        private readonly ILinkBuilder _linkBuilder;

        public TemplateBuilder(ILinkBuilder linkBuilder)
        {
            _linkBuilder = linkBuilder;
        }

        public ITemplateBuilder<TTemplate> Create()
        {
            _templateData = Activator.CreateInstance<TTemplate>();

            _template = new ResourceBuilderResource<TTemplate>
            {
                Data = BuildHelper.BuildTemplateData(_templateData),
                Meta = BuildHelper.BuildMeta(_templateData),
                Links = _linkBuilder.BuildLinks(_templateData)
            };

            BuildHelper.AddSearchQueryParams(_template.Links, _templateData);

            return this;
        }

        public ITemplateBuilder<TTemplate> WithTemplateMeta()
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