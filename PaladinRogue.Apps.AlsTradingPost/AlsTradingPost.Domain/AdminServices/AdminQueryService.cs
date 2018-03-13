﻿using System;
using System.Collections.Generic;
using AlsTradingPost.Domain.AdminServices.Interfaces;
using AlsTradingPost.Domain.AdminServices.Models;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Persistence.Interfaces;
using AutoMapper;

namespace AlsTradingPost.Domain.AdminServices
{
    public class AdminQueryService : IAdminQueryService
    {
        private readonly IMapper _mapper;
        private readonly IAdminRepository _adminRepository;

        public AdminQueryService(IMapper mapper, IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        public AdminProjection Get(Guid id)
        {
            return _mapper.Map<Admin, AdminProjection>(_adminRepository.GetById(id));
        }

        public IList<AdminProjection> GetAll()
        {
            return _mapper.Map<IList<Admin>, IList<AdminProjection>>(_adminRepository.Get());
        }
    }
}
