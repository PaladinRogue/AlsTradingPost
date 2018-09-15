using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Common.Api.Routing
{
    public class ApplicationRoutingConvention : IControllerModelConvention
    {
        private readonly string _applcationName;
        private readonly int _jsonApiVersion;

        public ApplicationRoutingConvention(string applcationName, int jsonApiVersion)
        {
            _applcationName = applcationName;
            _jsonApiVersion = jsonApiVersion;
        }

        public void Apply(ControllerModel controller)
        {
            bool hasRouteAttributes = controller.Selectors.Any(selector =>
                selector.AttributeRouteModel != null);

            if (hasRouteAttributes)
            {
                return;
            }

            string template = $"api/v{_jsonApiVersion}/{_applcationName}/[controller]";


            foreach (SelectorModel selector in controller.Selectors)
            {
                selector.AttributeRouteModel = new AttributeRouteModel()
                {
                    Template = template
                };
            }
        }
    }
}
