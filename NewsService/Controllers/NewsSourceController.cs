using NewsService.Aggregators;
using NewsService.Models;
using NewsService.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace NewsService.Controllers
{
    public class NewsSourceController : ApiController
    {
        private readonly INewsAggregator _newsAggregator;

        public NewsSourceController(INewsAggregator newsAggregator)
        {
            _newsAggregator = newsAggregator;
        }

        // POST api/news
        [HttpPost]
        public IHttpActionResult Post([FromUri] string sourceKey)
        {
            if (!_newsAggregator.IsSourceRegistered(sourceKey))
            {
                var source = NewsSourceFactory.Get(sourceKey);
                _newsAggregator.Add(source);
                return Ok(source);
            }
            return BadRequest("Source is already registered");
        }

        public IHttpActionResult Put(int id, [FromBody] IList<News> newsList)
        {
            var source = _newsAggregator.GetSource(id);
            if(source == null ) return BadRequest("Source is not registered");
            source.AddNews(newsList);
            return Ok(source);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromUri] string sourceKey)
        {
            if (!_newsAggregator.IsSourceRegistered(sourceKey))
                return BadRequest("Source is not registered");
            _newsAggregator.Unregister(sourceKey);
            return Ok();
        }
    }

    public class NewsSourceFactory
    {
        public static INewsSource Get(string sourceKey)
        {
            if (sourceKey.Equals("InternalNews", StringComparison.OrdinalIgnoreCase))
                return new InternalNewsSource { Id = 1, Name = "Internal News", Key = sourceKey };
            if (sourceKey.Equals("PtiNews", StringComparison.OrdinalIgnoreCase))
                return new PtiNewsSource { Id = 2, Name = "Press Trust of India News", Key = sourceKey };
            if (sourceKey.Equals("GoogleNews", StringComparison.OrdinalIgnoreCase))
                return new GoogleNewsSource { Id = 3, Name = "Google News", Key = sourceKey };
            throw new ArgumentException("Invalid Source Key");
        }
    }
}