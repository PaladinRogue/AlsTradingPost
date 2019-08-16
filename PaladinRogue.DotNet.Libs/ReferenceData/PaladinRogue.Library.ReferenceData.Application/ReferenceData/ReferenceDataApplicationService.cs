using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaladinRogue.Library.Core.Application.Transactions;
using PaladinRogue.Library.ReferenceData.Application.ReferenceData.Models;
using PaladinRogue.Library.ReferenceData.Domain.Persistence;
using PaladinRogue.Library.ReferenceData.Domain.Projections;

namespace PaladinRogue.Library.ReferenceData.Application.ReferenceData
{
    public class ReferenceDataApplicationService : IReferenceDataApplicationService
    {
        private readonly IReferenceDataQueryRepository _referenceDataQueryRepository;

        private readonly ITransactionManager _transactionManager;

        public ReferenceDataApplicationService(
            IReferenceDataQueryRepository referenceDataQueryRepository,
            ITransactionManager transactionManager)
        {
            _referenceDataQueryRepository = referenceDataQueryRepository;
            _transactionManager = transactionManager;
        }

        public async Task<IEnumerable<ReferenceDataValueAdto>> GetAllAsync(GetAllReferenceDataAdto getAllReferenceDataAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                IEnumerable<ReferenceDataValueAdto> referenceDataValueAdtos = (await _referenceDataQueryRepository.GetAllAsync(getAllReferenceDataAdto.Type))
                    .Select(r => new ReferenceDataValueAdto
                    {
                        Id = r.Id,
                        Code = r.Code
                    });

                transaction.Commit();

                return referenceDataValueAdtos;
            }
        }

        public async Task<ReferenceDataValueAdto> GetAsync(GetReferenceDataAdto getReferenceDataAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                ReferenceDataValueProjection referenceDataValueProjection;

                if (getReferenceDataAdto.Id.HasValue)
                {
                    referenceDataValueProjection = await _referenceDataQueryRepository.GetByIdAsync(getReferenceDataAdto.Id.Value);
                }
                else
                {
                    referenceDataValueProjection = await _referenceDataQueryRepository.GetByCodeAsync(getReferenceDataAdto.Type, getReferenceDataAdto.Code);
                }

                transaction.Commit();

                return new ReferenceDataValueAdto
                {
                    Id = referenceDataValueProjection.Id,
                    Code = referenceDataValueProjection.Code
                };
            }
        }
    }
}