using System.Linq;
using Common.Api.Sorting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Common.Api.QueryString
{
    public class QueryStringSortConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            foreach (var parameter in action.Parameters)
            {
                if (parameter.ParameterType.GetInterfaces().Contains(typeof(ISortTemplate)))
                {
                    parameter.Action.Filters.Add(new SeparatedQueryStringFilter(nameof(ISortTemplate.Sort), ","));
                }
            }
        }
    }
}
