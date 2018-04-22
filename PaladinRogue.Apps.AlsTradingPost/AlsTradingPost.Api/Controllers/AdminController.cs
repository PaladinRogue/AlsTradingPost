using System;
using AlsTradingPost.Api.Admin;
using AlsTradingPost.Application.AdminApplication.Interfaces;
using AlsTradingPost.Application.AdminApplication.Models;
using AlsTradingPost.Setup.Infrastructure.Authorization;
using AutoMapper;
using Common.Api.Builders.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlsTradingPost.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(PersonaPolicies.Admin)]
    public class AdminController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAdminApplicationService _adminApplicationService;

        public AdminController(IMapper mapper, IAdminApplicationService adminApplicationService)
        {
            _adminApplicationService = adminApplicationService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            AdminResource resource = _mapper.Map<AdminAdto, AdminResource>(_adminApplicationService.Get(id));

            return new ObjectResult(
                ResourceBuilder<AdminResource>.Create(resource)
                    .WithResourceMeta()
                    .Build()
            );
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateAdminTemplate template)
        {
            AdminResource resource = _mapper.Map<AdminAdto, AdminResource>(
                _adminApplicationService.Create(_mapper.Map<CreateAdminTemplate, CreateAdminAdto>(template)));

            return new ObjectResult(
                ResourceTemplateBuilder<AdminResource, CreateAdminTemplate>.Create(resource, template)
                    .WithResourceMeta()
                    .WithTemplateMeta()
                    .Build()
            );
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] UpdateAdminTemplate template, Guid id)
        {
            AdminResource resource = _mapper.Map<AdminAdto, AdminResource>(_adminApplicationService.Update(_mapper.Map(
                template, new UpdateAdminAdto
                {
                    Id = id
                })));

            return new ObjectResult(
                ResourceTemplateBuilder<AdminResource, UpdateAdminTemplate>.Create(resource, template)
                    .WithResourceMeta()
                    .WithTemplateMeta()
                    .Build()
            );
        }
    }
}
