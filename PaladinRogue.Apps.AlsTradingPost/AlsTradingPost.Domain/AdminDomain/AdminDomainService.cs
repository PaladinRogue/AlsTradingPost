using System;
using AlsTradingPost.Domain.AdminDomain.Interfaces;
using AlsTradingPost.Domain.AdminDomain.Models;
using AlsTradingPost.Domain.Models;
using AutoMapper;

namespace AlsTradingPost.Domain.AdminDomain
{
    public class AdminDomainService : IAdminDomainService
    {
        private readonly IAdminQueryService _adminQueryService;
        private readonly IMapper _mapper;

        public AdminDomainService(IAdminQueryService adminQueryService, IMapper mapper)
        {
            _adminQueryService = adminQueryService;
            _mapper = mapper;
        }

        public AdminProjection GetById(Guid id)
        {
            return _mapper.Map<Admin, AdminProjection>(_adminQueryService.GetById(id));
        }
    }
}
