using System;
using AlsTradingPost.Domain.AdminDomain.Interfaces;
using AlsTradingPost.Domain.AdminDomain.Models;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using AutoMapper;
using Common.Domain.Exceptions;
using Common.Domain.Models;
using Microsoft.Extensions.Logging;

namespace AlsTradingPost.Domain.AdminDomain
{
    public class AdminCommandService : IAdminCommandService
    {
        private readonly ILogger<AdminCommandService> _logger;
        private readonly IMapper _mapper;
        private readonly IAdminRepository _adminRepository;

        public AdminCommandService(IMapper mapper, IAdminRepository adminRepository, ILogger<AdminCommandService> logger)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public AdminProjection Create(CreateAdminDdto entity)
        {
            try
            {
                Admin newAdmin = _mapper.Map(entity, AggregateFactory.CreateRoot<Admin>());

                _adminRepository.Add(newAdmin);

                return _mapper.Map<Admin, AdminProjection>(_adminRepository.GetById(newAdmin.Id));
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to create admin");
                throw new DomainException("Unable to create admin");
            }
        }

        public AdminProjection Update(UpdateAdminDdto entity)
        {
            try
            {
                _adminRepository.Update(_mapper.Map<UpdateAdminDdto, Admin>(entity));

                return _mapper.Map<Admin, AdminProjection>(_adminRepository.GetById(entity.Id));
            }
            catch (ConcurrencyDomainException e)
            {
                _logger.LogCritical(e, "Unable to update admin");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to update admin");
                throw new DomainException("Unable to update admin");
            }
        }
    }
}
