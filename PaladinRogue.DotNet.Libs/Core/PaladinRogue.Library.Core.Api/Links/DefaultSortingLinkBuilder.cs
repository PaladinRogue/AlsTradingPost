﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PaladinRogue.Library.Core.Api.Sorting;
using PaladinRogue.Library.Core.Common.Builders.Dictionaries;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Library.Core.Api.Links
{
    public class DefaultSortingLinkBuilder : ISortingLinkBuilder
    {
        public SortLink BuildLink(string name, string uri, ISortTemplate sortTemplate)
        {
            return _pageLink(name, uri, sortTemplate);
        }

        private PageLink _pageLink(string linkType, string pageLinkUri, ISortTemplate sortTemplate)
        {
            return new PageLink
            {
                Uri = pageLinkUri,
                Name = linkType,
                AllowVerbs = HttpVerb.Get,
                Sort = sortTemplate.Sort,
                QueryParams = _getAdditionalParams(sortTemplate)
            };
        }

        private IDictionary<string, object> _getAdditionalParams(ISortTemplate sortTemplate)
        {
            DictionaryBuilder<string, object> dictionaryBuilder = DictionaryBuilder<string, object>.Create();

            List<string> handledProperties = new List<string>
            {
                nameof(sortTemplate.Sort)
            };

            foreach (PropertyInfo propertyInfo in sortTemplate.GetType().GetProperties().Where(p => !handledProperties.Contains(p.Name)))
            {
                dictionaryBuilder.Add(propertyInfo.Name, propertyInfo.GetValue(sortTemplate).ToString());
            }

            return dictionaryBuilder.Build();
        }
    }
}