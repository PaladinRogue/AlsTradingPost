using System.Linq;
using System.Threading.Tasks;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.AddClaim;
using ApplicationManager.Domain.Identities.ChangeClaim;
using Common.ApplicationServices.Exceptions;
using Common.ApplicationServices.Transactions;
using Common.Domain.Exceptions;
using Common.Domain.Persistence;
using Microsoft.Extensions.Logging;

namespace ApplicationManager.ApplicationServices.Identities.Claims
{
    public class IdentityClaimsApplicationKernalService : IIdentityClaimsApplicationKernalService
    {
        private readonly ICommandRepository<Identity> _commandRepository;

        private readonly ITransactionManager _transactionManager;

        private readonly ILogger<IdentityClaimsApplicationKernalService> _logger;

        private readonly IAddIdentityClaimCommand _addIdentityClaimCommand;

        private readonly IChangeIdentityClaimCommand _changeIdentityClaimCommand;

        public IdentityClaimsApplicationKernalService(
            ICommandRepository<Identity> commandRepository,
            ITransactionManager transactionManager,
            ILogger<IdentityClaimsApplicationKernalService> logger,
            IAddIdentityClaimCommand addIdentityClaimCommand,
            IChangeIdentityClaimCommand changeIdentityClaimCommand)
        {
            _commandRepository = commandRepository;
            _transactionManager = transactionManager;
            _logger = logger;
            _addIdentityClaimCommand = addIdentityClaimCommand;
            _changeIdentityClaimCommand = changeIdentityClaimCommand;
        }

        public async Task UpdateAsync(UpdateIdentityClaimAdto updateIdentityClaimAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    Identity identity = await _commandRepository.GetByIdAsync(updateIdentityClaimAdto.IdentityId);

                    if (identity == null)
                    {
                        throw new BusinessApplicationException(ExceptionType.NotFound, $"Identity with id: {updateIdentityClaimAdto.IdentityId} does not exist");
                    }

                    Claim claim = identity.Claims.FirstOrDefault(c => c.Type == updateIdentityClaimAdto.Type);

                    if (claim == null)
                    {
                        await _addIdentityClaimCommand.ExecuteAsync(identity, new AddIdentityClaimCommandDdto
                        {
                            Type = updateIdentityClaimAdto.Type,
                            Value = updateIdentityClaimAdto.Value
                        });
                    }
                    else
                    {
                        //TODO Ask about this, should we pass the claim in directly or do the change through the identity
                        await _changeIdentityClaimCommand.ExecuteAsync(identity, new ChangeIdentityClaimCommandDdto
                        {
                            Type = updateIdentityClaimAdto.Type,
                            Value = updateIdentityClaimAdto.Value
                        });
                    }

                    await _commandRepository.UpdateAsync(identity);

                    transaction.Commit();
                }
                catch (DomainValidationRuleException e)
                {
                    _logger.LogInformation("Could not add identity claim", e);
                }
            }
        }
    }
}