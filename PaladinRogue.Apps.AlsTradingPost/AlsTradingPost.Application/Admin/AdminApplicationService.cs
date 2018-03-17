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

        public AdminAdto Get(Guid id)
        {
            return _mapper.Map<AdminProjection, AdminAdto> (_adminQueryService.Get(id));
        }

        public IList<AdminSummaryAdto> GetAll()
        {
            return _mapper.Map<IList<AdminSummaryProjection>, IList<AdminSummaryAdto>>(_adminQueryService.GetAll());
        }

        public AdminAdto Create(CreateAdminAdto admin)
        {
            var newAdmin = _mapper.Map<CreateAdminAdto, CreateAdminDdto>(admin);

            return _mapper.Map<AdminProjection, AdminAdto>(_adminCommandService.Create(newAdmin));
        }

        public AdminAdto Update(UpdateAdminAdto admin)
        {
            var updatedAdmin = _mapper.Map<UpdateAdminAdto, UpdateAdminDdto>(admin);

            return _mapper.Map<AdminProjection, AdminAdto>(_adminCommandService.Update(updatedAdmin));
        }
    }
}

