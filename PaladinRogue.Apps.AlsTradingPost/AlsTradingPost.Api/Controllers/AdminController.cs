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
        private readonly IResourceBuilder _resourceBuilder;
        private readonly IResourceTemplateBuilder _resourceTemplateBuilder;

        public AdminController(IMapper mapper,
            IAdminApplicationService adminApplicationService,
            IResourceBuilder resourceBuilder,
            IResourceTemplateBuilder resourceTemplateBuilder)
        {
            _adminApplicationService = adminApplicationService;
            _mapper = mapper;
            _resourceBuilder = resourceBuilder;
            _resourceTemplateBuilder = resourceTemplateBuilder;
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            AdminResource resource = _mapper.Map<AdminAdto, AdminResource>(_adminApplicationService.Get(id));

            return new ObjectResult(
                _resourceBuilder.Create(resource)
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
                _resourceTemplateBuilder.Create(resource, template)
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
                _resourceTemplateBuilder.Create(resource, template)
                    .WithResourceMeta()
                    .WithTemplateMeta()
                    .Build()
            );
        }
    }
}
