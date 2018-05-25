using System;
using AlsTradingPost.Domain.AdminDomain.Interfaces;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;

namespace AlsTradingPost.Domain.AdminDomain
{
    public class AdminQueryService : IAdminQueryService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminQueryService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public Admin GetById(Guid id)
        {
            return _adminRepository.GetById(id);
        }
    }
}
