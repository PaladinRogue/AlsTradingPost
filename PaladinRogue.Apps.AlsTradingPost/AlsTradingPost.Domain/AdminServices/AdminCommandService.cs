using System;
using AlsTradingPost.Domain.AdminServices.Interfaces;
using AlsTradingPost.Domain.AdminServices.Models;
using AlsTradingPost.Domain.Exceptions;
using AlsTradingPost.Domain.Logging;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Persistence.Interfaces;
using AutoMapper;

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
                var newAdmin = _mapper.Map(entity, new Admin());

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
            throw new NotImplementedException();
        }
    }
}
