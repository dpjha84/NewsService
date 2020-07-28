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
        /// <returns></returns
        [HttpGet]
        [Route("")]
        [Route("api/news")]
        public IHttpActionResult Get([FromUri] PagingInfo pagingInfo)
        {
            var pageNumber = pagingInfo != null ? pagingInfo.pageNumber : PagingInfo.defaultPageNumber;
            var pageSize = pagingInfo != null ? pagingInfo.pageSize : PagingInfo.defaultPageSize;
            return Ok(_newsAggregator.GetPagedData<News>(pageNumber, pageSize));
        }
    }
}
