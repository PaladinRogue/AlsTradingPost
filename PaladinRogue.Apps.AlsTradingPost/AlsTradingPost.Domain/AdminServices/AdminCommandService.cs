﻿using System;
using AlsTradingPost.Domain.AdminServices.Interfaces;
using AlsTradingPost.Domain.AdminServices.Models;
using AlsTradingPost.Domain.Exceptions;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Persistence.Interfaces;
using AutoMapper;
using Common.Domain.Logging;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.AdminServices
{
    public class AdminCommandService : Logger, IAdminCommandService
    {
        private readonly IMapper _mapper;
        private readonly IAdminRepository _adminRepository;

        public AdminCommandService(IMapper mapper, IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        public AdminProjection Create(CreateAdminDdto entity)
        {
            try
            {
                var newAdmin = _mapper.Map(entity, EntityFactory.CreateEntity<Admin>());

                _adminRepository.Add(newAdmin);

                return _mapper.Map<Admin, AdminProjection>(_adminRepository.GetById(newAdmin.Id));
            }
            catch (Exception e)
            {
                Log.Error(e);
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
            catch (Exception e)
            {
                Log.Error(e);
                throw new DomainException("Unable to update admin");
            }
        }
    }
}
