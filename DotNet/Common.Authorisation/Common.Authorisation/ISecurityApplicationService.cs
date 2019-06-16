using System;
using Common.Application.Exceptions;

namespace Common.Authorisation
{
    public interface ISecurityApplicationService
    {
        /// <param name="function">The method to execute</param>
        /// <param name="authorisationContext">The authorisation context</param>
        /// <exception cref="BusinessApplicationException"></exception>
        TOut Secure<TOut>(Func<TOut> function, IAuthorisationContext authorisationContext);
    }
}
