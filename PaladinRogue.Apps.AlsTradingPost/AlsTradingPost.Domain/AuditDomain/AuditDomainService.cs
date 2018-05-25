using System;
using AlsTradingPost.Domain.AuditDomain.Interfaces;
using AlsTradingPost.Domain.AuditDomain.Models;
using AlsTradingPost.Domain.Models;
using AutoMapper;
using Common.Domain.Models;
using Newtonsoft.Json;

namespace AlsTradingPost.Domain.AuditDomain
{
    public class AuditDomainService : IAuditDomainService
    {
        private readonly IMapper _mapper;
        private readonly IAuditCommandService _auditCommandService;

        public AuditDomainService(IMapper mapper,
            IAuditCommandService auditCommandService)
        {
            _auditCommandService = auditCommandService;
            _mapper = mapper;
        }

        public void AuditEntity(AuditEntityDdto auditEntity)
        {
            Audit newAudit = _mapper.Map(auditEntity, AggregateFactory.CreateRoot<Audit>());

            newAudit.Timestamp = DateTime.UtcNow;
            newAudit.EntityId = auditEntity.Entity.Id;
            newAudit.AuditedObject = JsonConvert.SerializeObject(auditEntity.Entity, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

            _auditCommandService.Create(newAudit);
        }
    }
}
