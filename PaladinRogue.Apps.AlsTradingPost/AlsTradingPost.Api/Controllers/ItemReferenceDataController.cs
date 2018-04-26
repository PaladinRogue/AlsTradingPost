﻿using System;
using AlsTradingPost.Api.ItemReferenceData;
using AlsTradingPost.Application.ItemReferenceDataApplication.Interfaces;
using AlsTradingPost.Application.ItemReferenceDataApplication.Models;
using AlsTradingPost.Setup.Infrastructure.Authorization;
using AutoMapper;
using Common.Api.Builders.Resource;
using Common.Api.Builders.Template;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlsTradingPost.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(PersonaPolicies.Player)]
    public class ItemReferenceDataController : Controller
    {
        private readonly IItemReferenceDataApplicationService _itemReferenceDataApplicationService;
        private readonly ITemplateBuilder _templateBuilder;
        private readonly ICollectionResourceBuilder<ItemReferenceDataSummaryResource> _collectionResourceBuilder;

        public ItemReferenceDataController(
            IItemReferenceDataApplicationService itemReferenceDataApplicationService,
            ITemplateBuilder templateBuilder, ICollectionResourceBuilder<ItemReferenceDataSummaryResource> collectionResourceBuilder)
        {
            _itemReferenceDataApplicationService = itemReferenceDataApplicationService;
            _templateBuilder = templateBuilder;
            _collectionResourceBuilder = collectionResourceBuilder;
        }

        [Route("{id}", Name = RouteDictionary.ItemReferenceDataGetById)]
        public IActionResult GetById(Guid id)
        {
            return new ObjectResult(null);
        }
        
        [HttpGet(Name = RouteDictionary.ItemReferenceDataGet)]
        public IActionResult Get(ItemReferenceDataSearchTemplate itemReferenceDataSearchTemplate)
        {
            ItemReferenceDataPagedCollectionAdto result = _itemReferenceDataApplicationService.Search(Mapper.Map<ItemReferenceDataSearchTemplate, ItemReferenceDataSearchAdto>(itemReferenceDataSearchTemplate));

            ItemReferenceDataPagedCollectionResource itemReferenceDataPagedCollectionResource =
                Mapper.Map<ItemReferenceDataPagedCollectionAdto, ItemReferenceDataPagedCollectionResource>(result);
            
            return new ObjectResult(
                _collectionResourceBuilder.Create(itemReferenceDataPagedCollectionResource, itemReferenceDataSearchTemplate)
                    .WithTemplateMeta()
                    .WithResourceMeta()
                    .WithSummaryResourceMeta()
                    .WithSorting()
                    .Build()
            );
        }

        [Route("searchTemplate", Name = RouteDictionary.ItemReferenceDataSearchTemplate)]
        public IActionResult GetSearchTemplate()
        {
            return new ObjectResult(
                _templateBuilder.Create<ItemReferenceDataSearchTemplate>()
                    .WithTemplateMeta()
                    .Build()
            );
        }
    }
}