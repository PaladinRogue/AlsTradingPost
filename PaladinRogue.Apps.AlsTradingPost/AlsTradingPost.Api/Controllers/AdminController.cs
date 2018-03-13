using System;
using System.Collections.Generic;
using AlsTradingPost.Api.Request.Admin;
using AlsTradingPost.Api.Resources.Admin;
using AlsTradingPost.Application.Admin.Interfaces;
using AlsTradingPost.Application.Admin.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AlsTradingPost.Api.Controllers
{
    [Route("api/[controller]")]
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
                _mapper.Map<IList<AdminAdto>, IList<AdminResource>>(_adminApplicationService.GetAdmins())
            );
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return new ObjectResult(
                _mapper.Map<AdminAdto, AdminResource>(_adminApplicationService.GetAdminById(id))
            ); 
        }

        [HttpPost]
        public IActionResult Post([FromBody]AdminRequestDto request)
        {
            return new ObjectResult(
                _mapper.Map<AdminAdto, AdminResource>(_adminApplicationService.Create(_mapper.Map<AdminRequestDto, CreateAdminAdto>(request)))
            ); 
        }
    }
}
