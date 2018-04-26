﻿using System;
using AlsTradingPost.Domain.AuditDomain.Interfaces;
using AlsTradingPost.Domain.AuditDomain.Models;
using AlsTradingPost.Domain.Persistence;
using AutoMapper;
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
            try
            {
                Domain.Models.Audit newAudit = _mapper.Map(auditEntity, EntityFactory.CreateEntity<Domain.Models.Audit>());

                newAudit.Timestamp = DateTime.UtcNow;
                newAudit.EntityId = auditEntity.Entity.Id;
                newAudit.AuditedObject = JsonConvert.SerializeObject(auditEntity.Entity, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

                _auditRepository.Add(newAudit);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to audit entity");
            }
        }
    }
}