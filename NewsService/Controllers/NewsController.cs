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
    public class NewsController : ApiController
    {
        private readonly INewsAggregator _newsAggregator;

        public NewsController(INewsAggregator newsAggregator)
        {
            _newsAggregator = newsAggregator;
        }

        // GET api/news
        public IEnumerable<News> Get([FromUri] PagingInfo pagingInfo)
        {
            var source = new InternalNewsSource();
            var json = JsonConvert.SerializeObject(source);
            return _newsAggregator
                //.Add(new InternalNewsSource())
                //.Add(new ExternalNewsSource())
                //.Add(new ExternalNewsSource2())
                .GetPagedData<News>(pagingInfo);
        }

        
    }

    public class NewsSource1
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
