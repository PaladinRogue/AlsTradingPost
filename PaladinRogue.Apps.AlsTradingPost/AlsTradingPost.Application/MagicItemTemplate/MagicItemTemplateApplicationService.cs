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
        private readonly IMagicItemTemplateDomainService _magicItemTemplateDomainService;
        private readonly IValidator<MagicItemTemplateSearchAdto> _magicItemTemplateSearchValidator;

        public MagicItemTemplateApplicationService(IMagicItemTemplateDomainService magicItemTemplateDomainService,
            IValidator<MagicItemTemplateSearchAdto> magicItemTemplateSearchValidator)
        {
            _magicItemTemplateDomainService = magicItemTemplateDomainService;
            _magicItemTemplateSearchValidator = magicItemTemplateSearchValidator;
        }

        public MagicItemTemplatePagedCollectionAdto Search(MagicItemTemplateSearchAdto magicItemTemplateSearchAdto)
        {
            _magicItemTemplateSearchValidator.ValidateAndThrow(magicItemTemplateSearchAdto);
            
            MagicItemTemplatePagedCollectionDdto result = _magicItemTemplateDomainService.GetPage(
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

