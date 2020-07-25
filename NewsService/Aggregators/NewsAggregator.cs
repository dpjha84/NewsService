using NewsService.Models;
using NewsService.Paging;
using NewsService.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsService.Aggregators
{
    public interface INewsSourceObserver
    {
        void Refresh();
    }
    public class NewsAggregator : INewsAggregator, INewsSourceObserver
    {
        private IEnumerable<News> _aggregatedNews = new List<News>();
        private readonly IList<INewsSource> _newsSources;
        private readonly ICrossSourceAggregationStrategy _aggregationStrategy;

        public NewsAggregator(ICrossSourceAggregationStrategy aggregationStrategy)
        {
            _newsSources = new List<INewsSource>();
            _aggregationStrategy = aggregationStrategy;
        }

        public bool IsSourceRegistered(string key)
        {
            return _newsSources.Any(n => n.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));
        }

        public INewsSource GetSource(int id)
        {
            return _newsSources.FirstOrDefault(n => n.Id == id);
        }

        public INewsSource GetSource(string key)
        {
            return _newsSources.FirstOrDefault(n => n.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));
        }

        public bool IsSourceRegistered(int id)
        {
            return _newsSources.Any(n => n.Id == id);
        }

        public INewsAggregator Add(INewsSource source)
        {
            _newsSources.Add(source);
            source.Attach(this);
            Refresh();
            return this;
        }

        public void Unregister(string key)
        {
            var source = _newsSources.FirstOrDefault(n => n.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));
            if (source == null) return;
            _newsSources.Remove(source);
            Refresh();
        }

        public PagedList<T> GetPagedData<T>(PagingInfo info)
        {
            var items = _aggregatedNews.Skip((info.pageNumber - 1) * info.pageSize).Take(info.pageSize);
            return new PagedList<T>((IEnumerable<T>)items, _aggregatedNews.Count(), info.pageNumber, info.pageSize);
        }

        public void Refresh()
        {
            _aggregatedNews = _aggregationStrategy.Aggregate(_newsSources);
        }

        private IEnumerable<News> AggregatedNews
        {
            get
            {
                if (_aggregatedNews == null) _aggregatedNews = _aggregationStrategy.Aggregate(_newsSources);
                return _aggregatedNews;
            }
        }
    }
}