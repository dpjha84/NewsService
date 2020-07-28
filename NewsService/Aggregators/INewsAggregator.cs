using NewsService.Models;
using NewsService.Paging;
using NewsService.Sources;
using System.Collections.Generic;

namespace NewsService.Aggregators
{
    /// <summary>
    /// Interface for news aggregator
    /// </summary>
    public interface INewsAggregator
    {
        /// <summary>
        /// Aggregate news from sources
        /// </summary>
        /// <returns></returns>
        IEnumerable<News> Aggregate();
    }
}