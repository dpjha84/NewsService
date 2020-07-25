using NewsService.Models;
using NewsService.Sources;
using System.Collections.Generic;
using System.Linq;

namespace NewsService.Aggregators
{
    public class PriorityNewsFirstAggregationStrategy : ICrossSourceAggregationStrategy
    {
        public IEnumerable<News> Aggregate(IList<INewsSource> newsSources)
        {
            var priorties = Enumerable.Empty<News>();
            priorties = newsSources.Aggregate(priorties, (current, list)
                => current.Concat(list.News.Where(n => n.IsPriority)));

            var ads = Enumerable.Empty<News>();
            ads = newsSources.Aggregate(ads, (current, list)
                => current.Concat(list.News.Where(n => n.Category == NewsCategory.Advertisements)));

            var combined = Enumerable.Empty<News>();
            combined = newsSources.Aggregate(combined, (current, list)
                => current.Concat(list.News.Where(n => n.IsPriority)));

            return newsSources.Aggregate(combined, (current, list)
                => current.Concat(list.News.Where(n => !n.IsPriority)));
        }
    }
}