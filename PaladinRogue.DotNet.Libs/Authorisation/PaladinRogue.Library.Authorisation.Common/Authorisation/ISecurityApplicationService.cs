using System;
using System.Threading.Tasks;
using PaladinRogue.Library.Authorisation.Common.Contexts;

namespace PaladinRogue.Library.Authorisation.Common.Authorisation
{
    public interface ISecurityApplicationService
    {
        /// <param name="function">The method to execute</param>
        /// <param name="authorisationContext">The authorisation context</param>
        /// <exception cref="BusinessApplicationException"></exception>
        Task<TOut> SecureAsync<TOut>(Func<Task<TOut>> function,
            IAuthorisationContext authorisationContext);

        /// <param name="action">The method to execute</param>
        /// <param name="authorisationContext">The authorisation context</param>
        /// <exception cref="BusinessApplicationException"></exception>
        Task SecureAsync(Func<Task> action,
            IAuthorisationContext authorisationContext);
    }
}
