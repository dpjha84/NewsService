using NewsService.Models;
using NewsService.Sources;
using System.Collections.Generic;

namespace NewsService.Aggregators
{
    public interface ICrossSourceAggregationStrategy
    {
        IEnumerable<News> Aggregate(IEnumerable<INewsSource> newsSources);
    }
}