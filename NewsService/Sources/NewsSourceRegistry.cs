using NewsService.Sources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsService.Controllers
{
    public class NewsSourceRegistry<T> : INewsSourceRegistry<T> where T : INewsSource, new()
    {
        private readonly Dictionary<string, T> _sourceMap;
        public event EventHandler<EventArgs> OnSourceRegistered;
        public event EventHandler<EventArgs> OnSourceUnregistered;

        public NewsSourceRegistry()
        {
            _sourceMap = new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase);
        }

        public bool IsRegistered(string sourceKey)
        {
            return _sourceMap.ContainsKey(sourceKey);
        }

        public bool IsRegistered(int sourceId)
        {
            return GetSource(sourceId) != null;
        }

        public T GetSource(string sourceKey)
        {
            if (!_sourceMap.ContainsKey(sourceKey)) return default;
            return _sourceMap[sourceKey];
        }

        public T GetSource(int id)
        {
            return _sourceMap.Values.FirstOrDefault(x => x.Id == id);
        }

        public T Register(string sourceKey)
        {
            if (IsRegistered(sourceKey)) return default;
            var highestId = _sourceMap.Any() ? _sourceMap.Select(x => x.Value.Id).Max() : 0;
            var source = new T { Id = highestId + 1, Key = sourceKey };
            _sourceMap.Add(sourceKey, source);
            OnSourceRegistered?.Invoke(source, new EventArgs());
            return source;
        }

        public void Unregister(string sourceKey)
        {
            if (!_sourceMap.ContainsKey(sourceKey)) return;
            var source = _sourceMap[sourceKey];
            _sourceMap.Remove(sourceKey);
            OnSourceUnregistered?.Invoke(source, new EventArgs());
        }

        public IEnumerable<T> GetNewsSources()
        {
            return _sourceMap.Values;
        }
    }
}