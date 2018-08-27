using System;
using AlsTradingPost.Domain.AdminDomain.Interfaces;
using AlsTradingPost.Domain.AdminDomain.Models;
using AlsTradingPost.Domain.Models;
using AutoMapper;
using Common.Domain.Services.Query;

namespace AlsTradingPost.Domain.AdminDomain
{
    public class AdminDomainService : IAdminDomainService
    {
        private readonly IQueryService<Admin> _adminQueryService;
        private readonly IMapper _mapper;

        public AdminDomainService(IQueryService<Admin> adminQueryService, IMapper mapper)
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
