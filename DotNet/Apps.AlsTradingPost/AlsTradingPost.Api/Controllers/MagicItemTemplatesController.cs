using System;
using AlsTradingPost.Api.MagicItemTemplate;
using AlsTradingPost.Application.MagicItemTemplate.Interfaces;
using AlsTradingPost.Application.MagicItemTemplate.Models;
using AlsTradingPost.Setup.Infrastructure.Authorisation;
using AlsTradingPost.Setup.Infrastructure.Routing;
using AutoMapper;
using Common.Api.Builders.Resource;
using Common.Application.Authorisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlsTradingPost.Api.Controllers
{
    [Authorize(PersonaPolicies.Trader)]
    public class MagicItemTemplatesController : Controller
    {
        private readonly ISecure<IMagicItemTemplateApplicationService> _secureMagicItemTemplateApplicationService;
        private readonly IResourceBuilder _resourceBuilder;

        public MagicItemTemplatesController(
            ISecure<IMagicItemTemplateApplicationService> magicItemTemplateApplicationService,
            ISecure<IMagicItemTemplateApplicationService> secureMagicItemTemplateApplicationService,
            IResourceBuilder resourceBuilder)
        {
            _secureMagicItemTemplateApplicationService = secureMagicItemTemplateApplicationService;
            _resourceBuilder = resourceBuilder;
        }

        [Route("{id}", Name = RouteDictionary.MagicItemTemplateGetById)]
        public IActionResult GetById(Guid id)
        {
            return new ObjectResult(null);
        }

        [HttpGet(Name = RouteDictionary.MagicItemTemplateGet)]
        public IActionResult Get(MagicItemTemplateSearchTemplate magicItemTemplateSearchTemplate)
        {
            MagicItemTemplatePagedCollectionAdto result =
                _secureMagicItemTemplateApplicationService.Service.Search(
                    Mapper.Map<MagicItemTemplateSearchTemplate, MagicItemTemplateSearchAdto>(
                        magicItemTemplateSearchTemplate));

            MagicItemTemplates magicItemTemplates =
                Mapper.Map<MagicItemTemplatePagedCollectionAdto, MagicItemTemplates>(result);

            return new ObjectResult(
                _resourceBuilder.Build(magicItemTemplates, magicItemTemplateSearchTemplate)
            );
        }

        [Route("searchTemplate", Name = RouteDictionary.MagicItemTemplateSearchTemplate)]
        public IActionResult GetSearchTemplate()
        {
            return new ObjectResult(
                _resourceBuilder.Build(new MagicItemTemplateSearchTemplate())
            );
        }
    }
}
