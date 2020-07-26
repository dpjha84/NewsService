using NewsService.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsService.Aggregators
{
    public interface IPagedNewsAggregator : INewsAggregator, IPagedData
    {
    }
}