using System;
using Common.Application.Exceptions;

namespace Common.Application.Authorisation
{
    public interface ISecurityApplicationService
    {
        /// <param name="function">The method to execute</param>
        /// <param name="authorisationRule">The authorisation rule</param>
        /// <exception cref="BusinessApplicationException"></exception>
        TOut Secure<TOut>(Func<TOut> function, IAuthorisationRule authorisationRule);
    }
}
