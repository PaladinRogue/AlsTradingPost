using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gateway.Application.Applications
{
    public interface IApplicationApplicationService
    {
        Task<IEnumerable<ApplicationAdto>> GetAllAsync();
    }
}