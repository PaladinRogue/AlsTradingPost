using System;
using AlsTradingPost.Domain.AuditDomain.Interfaces;
using AlsTradingPost.Domain.AuditDomain.Models;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using AutoMapper;
using Common.Domain.Exceptions;
using Common.Domain.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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

        public void AuditEntity(AuditEntityDdto auditEntity)
        {
            Audit newAudit = null;
            try
            {
                newAudit = _mapper.Map(auditEntity, AggregateFactory.CreateRoot<Audit>());

                newAudit.Timestamp = DateTime.UtcNow;
                newAudit.EntityId = auditEntity.Entity.Id;
                newAudit.AuditedObject = JsonConvert.SerializeObject(auditEntity.Entity, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

                _auditRepository.Add(newAudit);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to audit entity");
                throw new CreateDomainException(newAudit, e);
            }
        }
    }
}
