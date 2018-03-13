using System;
using System.Collections.Generic;
using AlsTradingPost.Application.Admin.Interfaces;
using AlsTradingPost.Application.Admin.Models;
using AlsTradingPost.Domain.AdminServices.Interfaces;
using AlsTradingPost.Domain.AdminServices.Models;
using AutoMapper;

namespace AlsTradingPost.Application.Admin
{
    public class AdminApplicationService : IAdminApplicationService
    {
        private readonly IAdminCommandService _adminCommandService;
        private readonly IAdminQueryService _adminQueryService;
        private readonly IMapper _mapper;

        public AdminApplicationService(IMapper mapper, IAdminCommandService adminCommandService, IAdminQueryService adminQueryService)
        {
            _mapper = mapper;
            _adminCommandService = adminCommandService;
            _adminQueryService = adminQueryService;
        }

        public IList<AdminAdto> GetAdmins()
        {
            return _mapper.Map<IList<AdminProjection>, IList<AdminAdto>>(_adminQueryService.GetAll());
        }

        public AdminAdto GetAdminById(Guid id)
        {
            return _mapper.Map<AdminProjection, AdminAdto> (_adminQueryService.Get(id));
        }

        public AdminAdto Create(CreateAdminAdto admin)
        {
            var newAdmin = _mapper.Map<CreateAdminAdto, AdminDdto>(admin);

            var result = _adminCommandService.Create(newAdmin);

            return _mapper.Map<AdminDdto, AdminAdto>(newAdmin);
        }
    }
}

