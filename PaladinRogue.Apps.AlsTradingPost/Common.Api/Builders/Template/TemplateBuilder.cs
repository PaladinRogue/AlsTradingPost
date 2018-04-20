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
                Data = BuildHelper.BuildTemplateData(_templateData)
            };
        }

        public static TemplateBuilder<T> Create()
        {
            return new TemplateBuilder<T>();
        }

        public TemplateBuilder<T> WithMeta()
        {
            _template.Meta = BuildHelper.BuildMeta(_templateData);

            return this;
        }

        public IDictionary<string, object> Build()
        {
            return new Dictionary<string, object>
            {
                {
                    ResourceType.Data, new Dictionary<string, object>
                    {
                        {_template.Data.TypeName, _template.Data.Resource}
                    }
                },
                {
                    ResourceType.Meta, new Dictionary<string, object>
                    {
                        {
                            _template.Meta.TemplateTypeName, _template.Meta.Properties.ToDictionary(
                                p => p.Name,
                                p => p.Constraints.ToDictionary(
                                    c => c.Name,
                                    c => c.Value
                                ))
                        }
                    }
                }
            };
        }
    }
}