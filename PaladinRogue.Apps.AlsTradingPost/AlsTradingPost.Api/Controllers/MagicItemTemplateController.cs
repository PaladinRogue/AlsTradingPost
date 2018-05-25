using System;
using AlsTradingPost.Api.MagicItemTemplate;
using AlsTradingPost.Application.MagicItemTemplate.Interfaces;
using AlsTradingPost.Application.MagicItemTemplate.Models;
using AlsTradingPost.Setup.Infrastructure.Authorization;
using AutoMapper;
using Common.Api.Builders.Resource;
using Common.Api.Builders.Template;
using Common.Application.Authorisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlsTradingPost.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(PersonaPolicies.Trader)]
    public class MagicItemTemplateController : Controller
    {
        private readonly ISecure<IMagicItemTemplateApplicationService> _secureMagicItemTemplateApplicationService;
        private readonly ITemplateBuilder _templateBuilder;
        private readonly ICollectionResourceBuilder<MagicItemTemplateSummaryResource> _collectionResourceBuilder;

        public MagicItemTemplateController(
            ISecure<IMagicItemTemplateApplicationService> magicItemTemplateApplicationService,
            ITemplateBuilder templateBuilder,
            ICollectionResourceBuilder<MagicItemTemplateSummaryResource> collectionResourceBuilder,
            ISecure<IMagicItemTemplateApplicationService> secureMagicItemTemplateApplicationService)
        {
            _templateBuilder = templateBuilder;
            _collectionResourceBuilder = collectionResourceBuilder;
            _secureMagicItemTemplateApplicationService = secureMagicItemTemplateApplicationService;
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

            MagicItemTemplatePagedCollectionResource magicItemTemplatePagedCollectionResource =
                Mapper.Map<MagicItemTemplatePagedCollectionAdto, MagicItemTemplatePagedCollectionResource>(result);

            return new ObjectResult(
                _collectionResourceBuilder
                    .Create(magicItemTemplatePagedCollectionResource, magicItemTemplateSearchTemplate)
                    .WithTemplateMeta()
                    .WithResourceMeta()
                    .WithSummaryResourceMeta()
                    .WithSorting()
                    .Build()
            );
        }

        [Route("searchTemplate", Name = RouteDictionary.MagicItemTemplateSearchTemplate)]
        public IActionResult GetSearchTemplate()
        {
            return new ObjectResult(
                _templateBuilder.Create<MagicItemTemplateSearchTemplate>()
                    .WithTemplateMeta()
                    .Build()
            );
        }
    }
}
