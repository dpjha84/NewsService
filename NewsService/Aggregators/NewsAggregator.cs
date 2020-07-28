using NewsService.Controllers;
using NewsService.Models;
using NewsService.Paging;
using NewsService.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsService.Aggregators
{
    /// <summary>
    /// Paged News Aggregator
    /// </summary>
    public class PagedNewsAggregator : IPagedNewsAggregator, INewsSourceObserver
    {
        private IEnumerable<News> _aggregatedNews = Enumerable.Empty<News>();
        private readonly IEnumerable<INewsSource> _newsSources;
        private readonly ICrossSourceAggregationStrategy _aggregationStrategy;
        private readonly IPagingStrategy _pagingStrategy;

        public PagedNewsAggregator(INewsSourceRegistry<NewsSource> registry, ICrossSourceAggregationStrategy aggregationStrategy, IPagingStrategy pagingStrategy)
        {
            _newsSources = registry.GetNewsSources();
            registry.OnSourceRegistered += Registry_OnSourceRegistered;
            registry.OnSourceUnregistered += Registry_OnSourceUnregistered;
            _aggregationStrategy = aggregationStrategy;
            _pagingStrategy = pagingStrategy;
        }

        /// <summary>
        /// Get aggregated news for given page
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <returns></returns>
        public PagedList<T> GetPagedData<T>(PagingInfo info)
        {
            _pagingStrategy.PageSize = info.pageSize;
            var combined = _pagingStrategy.Arrange(_aggregatedNews);
            var items = combined.Skip((info.pageNumber - 1) * info.pageSize).Take(info.pageSize);
            return new PagedList<T>((IEnumerable<T>)items, combined.Count(), info.pageNumber, info.pageSize);
        }

        /// <summary>
        /// Refresh when news source data gets changed
        /// </summary>
        public void Refresh()
        {
            Aggregate();
        }

        /// <summary>
        /// Aggregate news based on the strategy
        /// </summary>
        /// <returns></returns>
        public IEnumerable<News> Aggregate()
        {
            _aggregatedNews = _aggregationStrategy.Aggregate(_newsSources);
            return _aggregatedNews;
        }

        private void Registry_OnSourceUnregistered(object sender, EventArgs e)
        {
            Aggregate();
        }

        private void Registry_OnSourceRegistered(object sender, EventArgs e)
        {
            (sender as INewsSource).Attach(this);
        }
    }
}