using NewsService.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsService.Aggregators
{
    /// <summary>
    /// News aggregator with aggregation and paging
    /// </summary>
    public interface IPagedNewsAggregator : INewsAggregator, IPagedData
    {
    }
}