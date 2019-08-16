using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using PaladinRogue.Library.Core.Api.QueryString;
using PaladinRogue.Library.Core.Api.Sorting;

namespace PaladinRogue.Library.Core.Api.Formats.JsonV1.Sorting
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
