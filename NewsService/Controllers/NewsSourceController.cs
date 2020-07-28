using NewsService.Aggregators;
using NewsService.Models;
using NewsService.Sources;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;

namespace NewsService.Controllers
{
    /// <summary>
    /// News Source Controller
    /// </summary>
    public class NewsSourceController : ApiController
    {
        const string sourceAlreadyRegistered = "Source is already registered";
        const string sourceNotRegistered = "Source is not registered";
        private readonly INewsSourceRegistry<NewsSource> _registry;

        public NewsSourceController(INewsSourceRegistry<NewsSource> registry)
        {
            _registry = registry;
        }

        /// <summary>
        /// Register a news source
        /// </summary>
        /// <param name="sourceKey">source key</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post([FromUri] string sourceKey)
        {
            var source = _registry.Register(sourceKey);
            if (source == null)
                return BadRequest(sourceAlreadyRegistered);
            return Ok(source);
        }

        /// <summary>
        /// Add news to a registred news source
        /// </summary>
        /// <param name="id">registered news source id</param>
        /// <param name="newsList">news list</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, [FromBody] IList<News> newsList)
        {
            var source = _registry.GetSource(id);
            if(source == null ) return BadRequest(sourceNotRegistered);
            source.AddNews(newsList);
            return Ok(source);
        }

        /// <summary>
        /// Unregister a news source
        /// </summary>
        /// <param name="sourceKey">source key</param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] string sourceKey)
        {
            if (!_registry.IsRegistered(sourceKey))
                return BadRequest(sourceAlreadyRegistered);
            _registry.Unregister(sourceKey);
            return Ok();
        }
    }
}