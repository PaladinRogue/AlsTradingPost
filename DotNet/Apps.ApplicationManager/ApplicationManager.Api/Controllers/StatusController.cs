﻿using ApplicationManager.Api.Status;
using Common.Api.Builders.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationManager.Api.Controllers
{
    [Route("api/[controller]")]
    public class StatusController : Controller
    {
        private readonly IResourceBuilder _resourceBuilder;

        public StatusController(IResourceBuilder resourceBuilder)
        {
            _resourceBuilder = resourceBuilder;
        }

        [AllowAnonymous]
        [HttpGet("", Name = RouteDictionary.Status)]
        public IActionResult Get()
        {
            return new ObjectResult(
                _resourceBuilder.Build(new StatusResource())
            );
        }
    }
}
