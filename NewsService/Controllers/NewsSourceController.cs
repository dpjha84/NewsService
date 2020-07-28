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
        [Route("api/newssource")]
        public IHttpActionResult Post([FromUri] string sourceKey = null)
        {
            if (string.IsNullOrWhiteSpace(sourceKey)) return BadRequest("Invalid Source Key");
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
        [HttpPut]
        [Route("api/newssource/{id}")]
        public IHttpActionResult Put(int id, [FromBody] IEnumerable<News> newsList)
        {
            if (newsList == null) return BadRequest("Invalid News List data");
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
        [Route("api/newssource")]
        public IHttpActionResult Delete([FromUri] string sourceKey = null)
        {
            if (string.IsNullOrWhiteSpace(sourceKey)) return BadRequest("Invalid Source Key");
            if (!_registry.IsRegistered(sourceKey))
                return BadRequest(sourceNotRegistered);
            _registry.Unregister(sourceKey);
            return Ok();
        }
    }
}