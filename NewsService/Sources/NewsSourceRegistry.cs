using NewsService.Sources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsService.Controllers
{
    /// <summary>
    /// News source registry
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NewsSourceRegistry<T> : INewsSourceRegistry<T> where T : INewsSource, new()
    {
        private readonly Dictionary<string, T> _sourceMap;
        public event EventHandler<EventArgs> OnSourceRegistered;
        public event EventHandler<EventArgs> OnSourceUnregistered;

        public NewsSourceRegistry()
        {
            _sourceMap = new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Check if a source key is registered as news source
        /// </summary>
        /// <param name="sourceKey"></param>
        /// <returns></returns>
        public bool IsRegistered(string sourceKey)
        {
            return _sourceMap.ContainsKey(sourceKey);
        }

        /// <summary>
        /// Check if a source id is registered as news source
        /// </summary>
        /// <param name="sourceKey"></param>
        /// <returns></returns>
        public bool IsRegistered(int sourceId)
        {
            return GetSource(sourceId) != null;
        }

        /// <summary>
        /// Get news source for given source key
        /// </summary>
        /// <param name="sourceKey"></param>
        /// <returns></returns>
        public T GetSource(string sourceKey)
        {
            if (!_sourceMap.ContainsKey(sourceKey)) return default;
            return _sourceMap[sourceKey];
        }

        /// <summary>
        /// Get news source for given source id
        /// </summary>
        /// <param name="sourceId"></param>
        /// <returns></returns>
        public T GetSource(int id)
        {
            return _sourceMap.Values.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Register a news source with source key
        /// </summary>
        /// <param name="sourceKey"></param>
        /// <returns></returns>
        public T Register(string sourceKey)
        {
            if (IsRegistered(sourceKey)) return default;
            var highestId = _sourceMap.Any() ? _sourceMap.Select(x => x.Value.Id).Max() : 0;
            var source = new T { Id = highestId + 1, Key = sourceKey };
            _sourceMap.Add(sourceKey, source);
            OnSourceRegistered?.Invoke(source, new EventArgs());
            return source;
        }

        /// <summary>
        /// Unregister a news source with source key
        /// </summary>
        /// <param name="sourceKey"></param>
        /// <returns></returns>
        public void Unregister(string sourceKey)
        {
            if (!_sourceMap.ContainsKey(sourceKey)) return;
            var source = _sourceMap[sourceKey];
            _sourceMap.Remove(sourceKey);
            OnSourceUnregistered?.Invoke(source, new EventArgs());
        }

        /// <summary>
        /// Get all registered News Sources
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetNewsSources()
        {
            return _sourceMap.Values;
        }
    }
}