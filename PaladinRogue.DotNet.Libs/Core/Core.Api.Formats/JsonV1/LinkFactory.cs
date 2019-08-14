using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaladinRogue.Library.Core.Api.Formats.JsonV1.Extensions;
using PaladinRogue.Library.Core.Api.Formats.JsonV1.Formats;
using PaladinRogue.Library.Core.Api.Links;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;
using Link = PaladinRogue.Library.Core.Api.Formats.JsonV1.Formats.Link;

namespace PaladinRogue.Library.Core.Api.Formats.JsonV1
{
    public class LinkFactory
    {
        public static Link Create(ILink link)
        {
            return new Link
            {
                Href = _buildFullUri(link, link.QueryParams),
                Meta = new LinkMeta
                {
                    AllowVerbs = _getStringVerbs(link.AllowVerbs)
                }
            };
        }

        private static string _buildFullUri(ILink link, IDictionary<string, object> queryParams)
        {
            StringBuilder stringbuilder = new StringBuilder(link.Uri);

            char separatorChar = '?';

            if (queryParams != null && queryParams.Any(p => p.Value != null))
            {
                foreach (KeyValuePair<string, object> keyValuePair in queryParams)
                {
                    if (keyValuePair.Value != null)
                    {
                        stringbuilder.Append($"{ separatorChar }{ _formatFilterParam(keyValuePair.Key) }={ keyValuePair.Value }");
                        separatorChar = '&';
                    }
                }
            }

            if (link is SortLink sortLink && sortLink.Sort != null)
            {
                stringbuilder.Append($"{separatorChar}{LinkPartType.Sort}={sortLink.Sort.ToCommaSeperatedString()}");
            }

            if (link is PageLink pageLink)
            {
                stringbuilder.Append($"{separatorChar}{_formatPagingParam(LinkPartType.PageOffset)}={pageLink.PageOffset}");
                stringbuilder.Append($"{separatorChar}{_formatPagingParam(LinkPartType.PageSize)}={pageLink.PageSize}");
            }

            return stringbuilder.ToString();
        }

        private static string _formatPagingParam(string paramName)
        {
            return $"page[{ paramName.ToLowerInvariant() }]";
        }

        private static string _formatFilterParam(string paramName)
        {
            return $"filter[{ paramName.ToLowerInvariant() }]";
        }

        private static IEnumerable<string> _getStringVerbs(HttpVerb httpVerbs)
        {
            IList<string> result = new List<string>();
            foreach (HttpVerb httpVerb in Enum.GetValues(typeof(HttpVerb)))
            {
                if ((httpVerb & httpVerbs) != 0) result.Add(httpVerb.ToString().ToUpperInvariant());
            }

            return result;
        }
    }
}
