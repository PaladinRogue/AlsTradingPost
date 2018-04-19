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

        public ItemReferenceDataController(IMapper mapper, IItemReferenceDataApplicationService itemReferenceDataApplicationService)
        {
            _itemReferenceDataApplicationService = itemReferenceDataApplicationService;
        }
        
        public IActionResult Get(ItemReferenceDataSearchTemplate itemReferenceDataSearchTemplate)
        {
            ItemReferenceDataPagedCollectionAdto result = _itemReferenceDataApplicationService.Search(Mapper.Map<ItemReferenceDataSearchTemplate, ItemReferenceDataSearchAdto>(itemReferenceDataSearchTemplate));

            ItemReferenceDataPagedCollectionResource itemReferenceDataPagedCollectionResource =
                Mapper.Map<ItemReferenceDataPagedCollectionAdto, ItemReferenceDataPagedCollectionResource>(result);
            
            return new ObjectResult(
                ResourceBuilder<ItemReferenceDataPagedCollectionResource>.Create(itemReferenceDataPagedCollectionResource)
                .WithMeta(itemReferenceDataSearchTemplate)
                .Build()
            );
        }

        [Route("searchTemplate")]
        public IActionResult GetSearchTemplate()
        {
            return new ObjectResult(
                TemplateBuilder<ItemReferenceDataSearchTemplate>.Create()
                    .WithMeta()
                    .Build()
            );
        }
    }
}
