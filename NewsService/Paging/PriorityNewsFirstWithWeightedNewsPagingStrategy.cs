using NewsService.Models;
using NewsService.Paging;
using System.Collections.Generic;
using System.Linq;

namespace NewsService.Paging
{
    /// <summary>
    /// Paging strategy with priority news first and news to ad ratio as 3:1
    /// </summary>
    public class PriorityNewsFirstWithWeightedNewsPagingStrategy : IPagingStrategy
    {
        public int PageSize { get; set; } = 8;

        private int NewsWeight { get; set; } = 3;

        private int AdsWeight { get; set; } = 1;

        /// <summary>
        /// Arrange
        /// </summary>
        /// <param name="newsList"></param>
        /// <returns></returns>
        public IEnumerable<News> Arrange(IEnumerable<News> newsList)
        {
            var priorties = newsList.Where(n => n.IsPriority);
            var normals = newsList.Where(n => !n.IsPriority && n.Category != NewsCategory.Advertisements);
            var ads = newsList.Where(n => n.Category == NewsCategory.Advertisements);

            var combined = Enumerable.Empty<News>();
            combined = combined.Concat(priorties);
            var offset = priorties.Count() % PageSize;

            int normalSkip = 0, adsSkip = 0;
            while (true)
            {
                var maxNormalNews = NewsWeight * (PageSize / (NewsWeight + AdsWeight));
                var list = normals.Skip(normalSkip).Take(maxNormalNews - offset);
                normalSkip += list.Count();
                combined = combined.Concat(list);
                if (list.Count() + offset == 0) break;
                var adsToPick = (list.Count() + offset) / NewsWeight;
                var adsList = ads.Skip(adsSkip).Take(adsToPick);
                combined = combined.Concat(adsList);
                adsSkip += adsList.Count();
                offset = 0;
            }
            return combined;
        }
    }
}