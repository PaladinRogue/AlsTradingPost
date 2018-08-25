using AlsTradingPost.Application.MagicItemTemplate.Interfaces;
using AlsTradingPost.Application.MagicItemTemplate.Models;
using AlsTradingPost.Domain.MagicItemTemplateDomain.Interfaces;
using AlsTradingPost.Domain.MagicItemTemplateDomain.Models;
using AutoMapper;
using Common.Application.Exceptions;
using Common.Application.Validation;
using Common.Domain.Sorting;
using FluentValidation;

namespace AlsTradingPost.Application.MagicItemTemplate
{
    public class MagicItemTemplateApplicationService : IMagicItemTemplateApplicationService
    {
        private readonly IMagicItemTemplateDomainService _magicItemTemplateDomainService;
        private readonly IValidator<MagicItemTemplateSearchAdto> _magicItemTemplateSearchValidator;
        private readonly IMapper _mapper;

        public MagicItemTemplateApplicationService(IMagicItemTemplateDomainService magicItemTemplateDomainService,
            IValidator<MagicItemTemplateSearchAdto> magicItemTemplateSearchValidator,
            IMapper mapper)
        {
            _magicItemTemplateDomainService = magicItemTemplateDomainService;
            _magicItemTemplateSearchValidator = magicItemTemplateSearchValidator;
            _mapper = mapper;
        }

        public MagicItemTemplatePagedCollectionAdto Search(MagicItemTemplateSearchAdto magicItemTemplateSearchAdto)
        {
            _magicItemTemplateSearchValidator.ValidateAndThrow(magicItemTemplateSearchAdto);
            
            try
            {
                MagicItemTemplatePagedCollectionDdto result = _magicItemTemplateDomainService.GetPage(
                    _mapper.Map<MagicItemTemplateSearchAdto, MagicItemTemplateSearchDdto>(magicItemTemplateSearchAdto),
                    magicItemTemplateSearchAdto.Sort,
                    i => string.IsNullOrEmpty(magicItemTemplateSearchAdto.Name) ||
                         i.Name.Contains(magicItemTemplateSearchAdto.Name)
                );

                return Mapper.Map<MagicItemTemplatePagedCollectionDdto, MagicItemTemplatePagedCollectionAdto>(result);
            }
            catch (PropertyNotSortableException e)
            {
                throw new BusinessValidationRuleApplicationException(ValidationResult.CreateFromException(e));
            }
        }
    }
}

