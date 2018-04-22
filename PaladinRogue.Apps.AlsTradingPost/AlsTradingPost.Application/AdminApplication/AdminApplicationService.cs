using System;
using AlsTradingPost.Application.AdminApplication.Interfaces;
using AlsTradingPost.Application.AdminApplication.Models;
using AlsTradingPost.Domain.AdminDomain.Interfaces;
using AlsTradingPost.Domain.AdminDomain.Models;
using AutoMapper;
using Common.Application.Exceptions;
using Common.Domain.Concurrency.Services.Interfaces;
using Common.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using ApplicationException = Common.Application.Exceptions.ApplicationException;

namespace AlsTradingPost.Application.AdminApplication
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
            return _mapper.Map<AdminProjection, AdminAdto> (_adminQueryService.GetById(id));
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
                throw new ApplicationException(ExceptionType.Concurrency, e);
            }
        }
    }
}

