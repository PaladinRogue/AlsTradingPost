using System.Collections.Generic;
using System.Linq;

namespace Common.Api.Routing
{
    public class Route
    {
        public string Template { get; }
        public IEnumerable<string> ControllerPolicies { get; }
        public IEnumerable<string> ActionPolicies { get; }
        public bool HasControllerPolicies => ControllerPolicies.Any();
        public bool HasActionPolicies => ActionPolicies.Any();
        
        private Route(string template, IEnumerable<string> controllerPolicies, IEnumerable<string> actionPolicies)
        {
            Template = template;
            ControllerPolicies = controllerPolicies;
            ActionPolicies = actionPolicies;
        }

        public static Route Create(string template, IEnumerable<string> controllerPolicies, IEnumerable<string> actionPolicies)
        {
            return new Route(template, controllerPolicies, actionPolicies);
        }
    }
}