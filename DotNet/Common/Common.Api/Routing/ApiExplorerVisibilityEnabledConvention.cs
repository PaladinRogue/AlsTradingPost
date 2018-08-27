using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Common.Api.Routing
{
    public class ApiExplorerVisibilityEnabledConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (ControllerModel applicationController in application.Controllers)
            {
                if (applicationController.ApiExplorer.IsVisible != null) continue;
                
                applicationController.ApiExplorer.IsVisible = true;
                applicationController.ApiExplorer.GroupName = applicationController.ControllerName;
            }
        }
    }
}