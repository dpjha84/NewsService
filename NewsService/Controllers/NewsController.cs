using NewsService.Aggregators;
using NewsService.Models;
using NewsService.Paging;
using NewsService.Sources;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace NewsService.Controllers
{
    /// <summary>
    /// News Controller
    /// </summary>
    public class NewsController : ApiController
    {
        private readonly IPagedNewsAggregator _newsAggregator;

        public NewsController(IPagedNewsAggregator newsAggregator)
        {
            _newsAggregator = newsAggregator;
        }

        /// <summary>
        /// Gets news based on page data
        /// </summary>
        /// <param name="pagingInfo"></param>
        /// <returns></returns>
        public IEnumerable<News> Get([FromUri] PagingInfo pagingInfo)
        {
            return _newsAggregator.GetPagedData<News>(pagingInfo);
        }
    }
}
