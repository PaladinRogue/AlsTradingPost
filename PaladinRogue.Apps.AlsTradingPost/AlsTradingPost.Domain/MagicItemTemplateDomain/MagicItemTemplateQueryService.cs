using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AlsTradingPost.Domain.MagicItemTemplateDomain.Interfaces;
using AlsTradingPost.Domain.MagicItemTemplateDomain.Models;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using AutoMapper;
using Common.Domain.Pagination.Interfaces;
using Common.Resources.Extensions;

namespace AlsTradingPost.Domain.MagicItemTemplateDomain
{
    public class MagicItemTemplateQueryService : IMagicItemTemplateQueryService
    {
        private readonly IMagicItemTemplateRepository _magicItemTemplateRepository;

        public MagicItemTemplateQueryService(IMagicItemTemplateRepository magicItemTemplateRepository)
        {
            _magicItemTemplateRepository = magicItemTemplateRepository;
        }

        public MagicItemTemplatePagedCollectionDdto GetPage(
            IPaginationDdto paginationDdto,
            Expression<Func<MagicItemTemplate, bool>> predicate = null,
            string orderBy = null,
            bool orderByAscending = true,
            string thenBy = null,
            bool? thenByAscending = null)
        {
            IEnumerable<MagicItemTemplate> results = _magicItemTemplateRepository.GetPage(
                paginationDdto.PageSize,
                paginationDdto.PageOffset,
                out int totalResults,
                orderBy.CreatePropertyAccessor<MagicItemTemplate>(),
                orderByAscending,
                predicate,
                thenBy.CreatePropertyAccessor<MagicItemTemplate>(),
                thenByAscending
            );

            return MagicItemTemplatePagedCollectionDdto.Create(
                Mapper.Map<IEnumerable<MagicItemTemplate>, IList<MagicItemTemplateSummaryProjection>>(results), totalResults
            );
        }
    }
}
