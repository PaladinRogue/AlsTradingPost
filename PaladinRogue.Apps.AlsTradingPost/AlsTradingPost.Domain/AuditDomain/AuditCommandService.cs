using System;
using AlsTradingPost.Domain.AuditDomain.Interfaces;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using AutoMapper;
using Common.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace AlsTradingPost.Domain.AuditDomain
{
    public class AuditCommandService : IAuditCommandService
    {
        private readonly ILogger<AuditCommandService> _logger;
        private readonly IMapper _mapper;
        private readonly IAuditRepository _auditRepository;

        public AuditCommandService(IMapper mapper,
            IAuditRepository auditRepository,
            ILogger<AuditCommandService> logger)
        {
            _auditRepository = auditRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public Guid Create(Audit entity)
        {
            try
            {
                _auditRepository.Add(entity);

                return entity.Id;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to audit entity");
                throw new CreateDomainException(entity, e);
            }
        }
    }
}
