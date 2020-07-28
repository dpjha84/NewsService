using NewsService.Models;
using NewsService.Sources;
using System.Collections.Generic;

namespace NewsService.Aggregators
{
    /// <summary>
    /// Interface for Cross Source Aggregation strategy
    /// </summary>
    public interface ICrossSourceAggregationStrategy
    {
        /// <summary>
        /// Aggregate
        /// </summary>
        /// <param name="newsSources"></param>
        /// <returns></returns>
        IEnumerable<News> Aggregate(IEnumerable<INewsSource> newsSources);
    }
}