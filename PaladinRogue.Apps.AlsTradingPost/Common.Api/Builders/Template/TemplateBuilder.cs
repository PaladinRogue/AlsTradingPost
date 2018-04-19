using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Api.Builders.Template
{
    public class TemplateBuilder<T>
    {
        private readonly TemplateBuilderTemplate<T> _template;
        private readonly T _templateData;

        private TemplateBuilder()
        {
            _templateData = Activator.CreateInstance<T>();
            _template = new TemplateBuilderTemplate<T>
            {
                Data = BuilderHelper.FormatTemplateData(_templateData)
            };
        }

        public static TemplateBuilder<T> Create()
        {
            return new TemplateBuilder<T>();
        }

        public TemplateBuilder<T> WithMeta()
        {
            _template.Meta = BuilderHelper.FormatTemplateMeta(_templateData);

            return this;
        }

        public Template Build()
        {
            return new Template
            {
                Data = new Dictionary<string, object>
                {
                    {  _template.Data.TemplateTypeName, _template.Data.Resource }
                },
                Meta = new Dictionary<string, object>
                {
                    {  _template.Meta.TemplateTypeName, _template.Meta.Properties.ToDictionary(
                        p => p.Name,
                        p => p.Constraints.ToDictionary(
                            c => c.Name,
                            c => c.Value
                        ))
                    }
                }
            };
        }
    }
}