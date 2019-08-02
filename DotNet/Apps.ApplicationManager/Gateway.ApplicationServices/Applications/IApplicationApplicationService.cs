using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gateway.ApplicationServices.Applications
{
    public interface IApplicationApplicationService
    {
        Task<IEnumerable<ApplicationAdto>> GetAllAsync();
    }
}