using System;
using System.Collections.Generic;
using AlsTradingPost.Application.Admin.Interfaces;
using AlsTradingPost.Application.Admin.Models;
using AlsTradingPost.Domain.AdminServices.Interfaces;
using AlsTradingPost.Domain.AdminServices.Models;
using AutoMapper;
using Common.Domain.ConcurrencyServices.Interfaces;
using Common.Domain.Exceptions;
using Common.Resources.Exceptions;
using Microsoft.Extensions.Logging;

namespace AlsTradingPost.Application.Admin
{
    public class AdminApplicationService : IAdminApplicationService
    {
        private readonly ILogger<AdminApplicationService> _logger;
        private readonly IAdminCommandService _adminCommandService;
        private readonly IAdminQueryService _adminQueryService;
        private readonly IMapper _mapper;
        private readonly IConcurrencyQueryService<IAdminQueryService> _concurrencyQueryService;

        public AdminApplicationService(ILogger<AdminApplicationService> logger,
            IMapper mapper,
            IAdminCommandService adminCommandService,
            IAdminQueryService adminQueryService,
            IConcurrencyQueryService<IAdminQueryService> concurrencyQueryService)
        {
            _mapper = mapper;
            _adminCommandService = adminCommandService;
            _adminQueryService = adminQueryService;
            _logger = logger;
            _concurrencyQueryService = concurrencyQueryService;
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
            CreateAdminDdto newAdmin = _mapper.Map<CreateAdminAdto, CreateAdminDdto>(admin);

            return _mapper.Map<AdminProjection, AdminAdto>(_adminCommandService.Create(newAdmin));
        }

        public AdminAdto Update(UpdateAdminAdto admin)
        {
            try
            {
                _concurrencyQueryService.CheckConcurrency(admin.Id, admin.Version);

                UpdateAdminDdto updatedAdmin = _mapper.Map<UpdateAdminAdto, UpdateAdminDdto>(admin);

                return _mapper.Map<AdminProjection, AdminAdto>(_adminCommandService.Update(updatedAdmin));
            }
            catch (ConcurrencyDomainException e)
            {
                _logger.LogInformation(e, "Concurrency exception");
                throw new AppException(ExceptionType.Concurrency, e);
            }
        }
    }
}

