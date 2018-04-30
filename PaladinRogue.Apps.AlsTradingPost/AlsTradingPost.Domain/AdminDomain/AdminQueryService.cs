using System;
using AlsTradingPost.Domain.AdminDomain.Interfaces;
using AlsTradingPost.Domain.AdminDomain.Models;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using AutoMapper;

namespace AlsTradingPost.Domain.AdminDomain
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

        public AdminProjection GetById(Guid id)
        {
            return _mapper.Map<Admin, AdminProjection>(_adminRepository.GetById(id));
        }
    }
}
