using NewsService.Models;
using NewsService.Paging;
using NewsService.Sources;
using System.Collections.Generic;

namespace NewsService.Aggregators
{
    public interface INewsAggregator
    {
        IEnumerable<News> Aggregate();
    }
}