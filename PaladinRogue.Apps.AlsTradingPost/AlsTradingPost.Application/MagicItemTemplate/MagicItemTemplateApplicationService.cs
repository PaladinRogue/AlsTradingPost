using AlsTradingPost.Application.MagicItemTemplate.Interfaces;
using AlsTradingPost.Application.MagicItemTemplate.Models;
using AlsTradingPost.Domain.MagicItemTemplateDomain.Interfaces;
using AlsTradingPost.Domain.MagicItemTemplateDomain.Models;
using AutoMapper;
using Common.Application.Validation;
using FluentValidation;

namespace AlsTradingPost.Application.MagicItemTemplate
{
    public class MagicItemTemplateApplicationService : IMagicItemTemplateApplicationService
    {
        private readonly IMagicItemTemplateQueryService _magicItemTemplateQueryService;
        private readonly IValidator<MagicItemTemplateSearchAdto> _magicItemTemplateSearchValidator;

        public MagicItemTemplateApplicationService(IMagicItemTemplateQueryService magicItemTemplateQueryService,
            IValidator<MagicItemTemplateSearchAdto> magicItemTemplateSearchValidator)
        {
            _magicItemTemplateQueryService = magicItemTemplateQueryService;
            _magicItemTemplateSearchValidator = magicItemTemplateSearchValidator;
        }

        public MagicItemTemplatePagedCollectionAdto Search(MagicItemTemplateSearchAdto magicItemTemplateSearchAdto)
        {
            _magicItemTemplateSearchValidator.ValidateAndThrow(magicItemTemplateSearchAdto);
            
            MagicItemTemplatePagedCollectionDdto result = _magicItemTemplateQueryService.GetPage(
                Mapper.Map<MagicItemTemplateSearchAdto, MagicItemTemplateSearchDdto>(magicItemTemplateSearchAdto),
                    i => string.IsNullOrEmpty(magicItemTemplateSearchAdto.Name) || i.Name.Contains(magicItemTemplateSearchAdto.Name),
                    magicItemTemplateSearchAdto.OrderBy,
                    magicItemTemplateSearchAdto.OrderByAscending,
                    magicItemTemplateSearchAdto.ThenBy,
                    magicItemTemplateSearchAdto.ThenByAscending
                );

            return Mapper.Map<MagicItemTemplatePagedCollectionDdto, MagicItemTemplatePagedCollectionAdto>(result);
        }
    }
}

