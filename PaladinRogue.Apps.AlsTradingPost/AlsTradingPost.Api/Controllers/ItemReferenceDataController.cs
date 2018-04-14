using System.Collections.Generic;
using AlsTradingPost.Api.ItemreferenceData;
using AlsTradingPost.Application.ItemReferenceDataApplication.Interfaces;
using AlsTradingPost.Application.ItemReferenceDataApplication.Models;
using AlsTradingPost.Resources.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlsTradingPost.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(PersonaPolicies.Player)]
    public class ItemReferenceDataController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IItemReferenceDataApplicationService _itemReferenceDataApplicationService;

        public ItemReferenceDataController(IMapper mapper, IItemReferenceDataApplicationService itemReferenceDataApplicationService)
        {
            _itemReferenceDataApplicationService = itemReferenceDataApplicationService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(
                _mapper.Map<IList<ItemReferenceDataSummaryAdto>, IList<ItemReferenceDataSummaryResource>>(_itemReferenceDataApplicationService.GetAll())
            );
        }
    }
}
