using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities.Queries;
using ApplicationManager.Domain.Identities.RegisterClientCredential;
using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.Login.ClientCredential
{
    public class ClientCredentialLoginCommand : IClientCredentialLoginCommand
    {
        private readonly IGetIdentityByClientCredentialIdentifierQuery _getIdentityByClientCredentialIdentifierQuery;

        private readonly IValidator<ClientCredentialLoginCommandDdto> _validator;

        public ClientCredentialLoginCommand(
            IGetIdentityByClientCredentialIdentifierQuery getIdentityByClientCredentialIdentifierQuery,
            IValidator<ClientCredentialLoginCommandDdto> validator)
        {
            _getIdentityByClientCredentialIdentifierQuery = getIdentityByClientCredentialIdentifierQuery;
            _validator = validator;
        }

        public Identity Execute(AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential,
            ClientCredentialLoginCommandDdto clientCredentialLoginCommandDdto)
        {
            _validator.ValidateAndThrow(clientCredentialLoginCommandDdto);

            Identity identity = _getIdentityByClientCredentialIdentifierQuery.Run(authenticationGrantTypeClientCredential, clientCredentialLoginCommandDdto.Identifier);

            if (identity == null)
            {
                identity = Identity.Create();

                identity.RegisterClientCredential(authenticationGrantTypeClientCredential, new RegisterClientCredentialDdto
                {
                    Identifier = clientCredentialLoginCommandDdto.Identifier
                });
            }

            identity.Login();

            return identity;
        }
    }
}