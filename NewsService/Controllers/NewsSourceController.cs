using NewsService.Aggregators;
using NewsService.Models;
using NewsService.Sources;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;

namespace NewsService.Controllers
{
    public class NewsSourceController : ApiController
    {
        private readonly INewsSourceRegistry<NewsSource> _registry;

        public NewsSourceController(INewsSourceRegistry<NewsSource> registry)
        {
            _registry = registry;
        }

        // POST api/news
        [HttpPost]
        public IHttpActionResult Post([FromUri] string sourceKey)
        {
            var source = _registry.Register(sourceKey);
            if (source == null)
                return BadRequest("Source is already registered");
            return Ok(source);
        }

        public IHttpActionResult Put(int id, [FromBody] IList<News> newsList)
        {
            var source = _registry.GetSource(id);
            if(source == null ) return BadRequest("Source is not registered");
            source.AddNews(newsList);
            return Ok(source);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromUri] string sourceKey)
        {
            if (!_registry.IsRegistered(sourceKey))
                return BadRequest("Source is not registered");
            _registry.Unregister(sourceKey);
            return Ok();
        }
    }
}