using NewsService.Models;
using NewsService.Sources;
using System.Collections.Generic;
using System.Linq;

namespace NewsService.Aggregators
{
    /// <summary>
    /// Priority News First Aggregation Strategy
    /// </summary>
    public class PriorityNewsFirstAggregationStrategy : ICrossSourceAggregationStrategy
    {
        public IEnumerable<News> Aggregate(IEnumerable<INewsSource> newsSources)
        {
            var priorties = Enumerable.Empty<News>();
            priorties = newsSources.Aggregate(priorties, (current, list)
                => current.Concat(list.News.Where(n => n.IsPriority)));

            var normals = Enumerable.Empty<News>();
            normals = newsSources.Aggregate(normals, (current, list)
                => current.Concat(list.News.Where(n => !n.IsPriority && n.Category != NewsCategory.Advertisements)));

            var ads = Enumerable.Empty<News>();
            ads = newsSources.Aggregate(ads, (current, list)
                => current.Concat(list.News.Where(n => n.Category == NewsCategory.Advertisements)));

            var combined = Enumerable.Empty<News>();
            return combined.Concat(priorties).Concat(normals).Concat(ads);
        }
    }
}