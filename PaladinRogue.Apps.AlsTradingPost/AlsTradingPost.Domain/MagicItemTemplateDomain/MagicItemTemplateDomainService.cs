using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AlsTradingPost.Domain.MagicItemTemplateDomain.Interfaces;
using AlsTradingPost.Domain.MagicItemTemplateDomain.Models;
using AlsTradingPost.Domain.Models;
using AutoMapper;
using Common.Domain.Pagination.Interfaces;
using Common.Domain.Services.Query;

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
            Expression<Func<MagicItemTemplate, bool>> predicate = null,
            string orderBy = null,
            bool orderByAscending = true,
            string thenBy = null,
            bool? thenByAscending = null)
        {
            IEnumerable<MagicItemTemplate> results = _magicItemTemplateQueryService.GetPage(
                paginationDdto,
                out int totalResults,
                predicate,
                orderBy,
                orderByAscending,
                thenBy,
                thenByAscending
            );

            return MagicItemTemplatePagedCollectionDdto.Create(
                _mapper.Map<IEnumerable<MagicItemTemplate>, IList<MagicItemTemplateSummaryProjection>>(results), totalResults
            );
        }
    }
}
