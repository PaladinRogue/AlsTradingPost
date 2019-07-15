using System.Threading.Tasks;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.AddOrChangeClaim;
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

        private readonly IAddOrChangeIdentityClaimCommand _addOrChangeIdentityClaimCommand;

        public IdentityClaimsApplicationKernalService(
            ICommandRepository<Identity> commandRepository,
            ITransactionManager transactionManager,
            ILogger<IdentityClaimsApplicationKernalService> logger,
            IAddOrChangeIdentityClaimCommand addOrChangeIdentityClaimCommand)
        {
            _commandRepository = commandRepository;
            _transactionManager = transactionManager;
            _logger = logger;
            _addOrChangeIdentityClaimCommand = addOrChangeIdentityClaimCommand;
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

                    await _addOrChangeIdentityClaimCommand.ExecuteAsync(identity, new AddOrChangeIdentityClaimCommandDdto
                    {
                        Type = updateIdentityClaimAdto.Type,
                        Value = updateIdentityClaimAdto.Value
                    });

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