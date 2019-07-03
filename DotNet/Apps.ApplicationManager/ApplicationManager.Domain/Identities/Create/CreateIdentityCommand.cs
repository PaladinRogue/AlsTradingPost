using System.Collections.Generic;
using ApplicationManager.Domain.Identities.Queries;
using Common.Domain.Exceptions;
using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.Create
{
    public class CreateIdentityCommand : ICreateIdentityCommand
    {
        public Identity Execute()
        {
            return Identity.Create();
        }
    }
}
