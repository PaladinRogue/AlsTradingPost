using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AlsTradingPost.Api.Responses
{
    public class Meta
    {
        public Meta(dynamic data)
        {

            //TODO reflectively get links - could do this from an attribute / named property?


            PropertyMeta = new List<PropertyMeta>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(data.GetType()))
            {
                List<string> constraints = new List<string>();
                if (property.Attributes.OfType<RequiredAttribute>().Any())
                {
                    constraints.Add("required");
                }


                //TODO check for all meta attributes


                if (constraints.Any())
                {
                    PropertyMeta.Add(new PropertyMeta(property.Name.ToCamelCase(), constraints));
                }
            }
        }

        public IList<PropertyMeta> PropertyMeta { get; set; }
        public IEnumerable<Link> Links { get; set; }
    }
}