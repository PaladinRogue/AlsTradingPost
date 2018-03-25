using Common.Domain.Models;

namespace Authentication.Domain.Models
{
    public class Application : Entity
    {
        public string Name { get; set; }
        public string Endpoint { get; set; }
        public string Secret { get; set; }
    }
}
