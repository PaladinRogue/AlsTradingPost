using System;
using System.Collections.Generic;
using AlsTradingPost.Api.Admin;
using AlsTradingPost.Application.AdminApplication.Interfaces;
using AlsTradingPost.Application.AdminApplication.Models;
using AlsTradingPost.Resources.Authorization;
using AutoMapper;
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

        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(
                _mapper.Map<IList<AdminSummaryAdto>, IList<AdminSummaryResource>>(_adminApplicationService.GetAll())
            );
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return new ObjectResult(
                _mapper.Map<AdminAdto, AdminResource>(_adminApplicationService.Get(id))
            ); 
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreateAdminTemplate request)
        {
            return new ObjectResult(
                _mapper.Map<AdminAdto, AdminResource>(_adminApplicationService.Create(_mapper.Map<CreateAdminTemplate, CreateAdminAdto>(request)))
            ); 
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody]UpdateAdminTemplate request, Guid id)
        {
            return new ObjectResult(
                _mapper.Map<AdminAdto, AdminResource>(_adminApplicationService.Update(_mapper.Map(request, new UpdateAdminAdto
                {
                    Id = id
                })))
            ); 
        }
    }
}
