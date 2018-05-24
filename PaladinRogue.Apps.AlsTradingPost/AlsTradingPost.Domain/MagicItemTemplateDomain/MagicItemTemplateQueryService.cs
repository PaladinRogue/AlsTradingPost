using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AlsTradingPost.Domain.MagicItemTemplateDomain.Interfaces;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
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

        public IEnumerable<MagicItemTemplate> GetPage(
            IPaginationDdto paginationDdto,
            out int totalResults,
            Expression<Func<MagicItemTemplate, bool>> predicate = null,
            string orderBy = null,
            bool orderByAscending = true,
            string thenBy = null,
            bool? thenByAscending = null)
        {
            return _magicItemTemplateRepository.GetPage(
                paginationDdto.PageSize,
                paginationDdto.PageOffset,
                out totalResults,
                orderBy.CreatePropertyAccessor<MagicItemTemplate>(),
                orderByAscending,
                predicate,
                thenBy.CreatePropertyAccessor<MagicItemTemplate>(),
                thenByAscending
            );
        }
    }
}
