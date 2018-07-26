using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AlsTradingPost.Domain.MagicItemTemplateDomain.Interfaces;
using AlsTradingPost.Domain.MagicItemTemplateDomain.Models;
using AlsTradingPost.Domain.Models;
using AutoMapper;
using Common.Domain.Pagination.Interfaces;
using Common.Domain.Services.Query;
using Common.Resources.Sorting;

namespace AlsTradingPost.Domain.MagicItemTemplateDomain
{
    public class MagicItemTemplateDomainService : IMagicItemTemplateDomainService
    {
        private readonly IQueryService<MagicItemTemplate> _magicItemTemplateQueryService;
        private readonly IMapper _mapper;

        public MagicItemTemplateDomainService(IQueryService<MagicItemTemplate> magicItemTemplateQueryService,
            IMapper mapper)
        {
            _magicItemTemplateQueryService = magicItemTemplateQueryService;
            _mapper = mapper;
        }

        public MagicItemTemplatePagedCollectionDdto GetPage(
            IPaginationDdto paginationDdto,
            IList<SortBy> sort,
            Expression<Func<MagicItemTemplate, bool>> predicate = null)
        {
            IEnumerable<MagicItemTemplate> results = _magicItemTemplateQueryService.GetPage(
                paginationDdto,
                out int totalResults,
                sort,
                predicate
            );

            return MagicItemTemplatePagedCollectionDdto.Create(
                _mapper.Map<IEnumerable<MagicItemTemplate>, IList<MagicItemTemplateSummaryProjection>>(results), totalResults
            );
        }
    }
}
