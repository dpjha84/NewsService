using NewsService.Aggregators;
using NewsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsService.Sources
{
    /// <summary>
    /// News source
    /// </summary>
    public class NewsSource : INewsSource
    {
        private readonly List<INewsSourceObserver> _observers = new List<INewsSourceObserver>();

        /// <summary>
        /// Registered news source id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Registered news source key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// All news for this news source
        /// </summary>
        public IEnumerable<News> News { get; private set; } = new List<News>();

        /// <summary>
        /// Add news
        /// </summary>
        /// <param name="news"></param>
        public void AddNews(IEnumerable<News> news)
        {
            News = News.Concat(news);
            Notify();
        }

        /// <summary>
        /// Add observer
        /// </summary>
        /// <param name="observer"></param>
        public void Attach(INewsSourceObserver observer)
        {
            _observers.Add(observer);
        }

        /// <summary>
        /// Remove observer
        /// </summary>
        /// <param name="observer"></param>
        public void Detach(INewsSourceObserver observer)
        {
            _observers.Remove(observer);
        }

        /// <summary>
        /// Notify all observers to refresh
        /// </summary>
        public void Notify()
        {
            _observers.ForEach(o => o.Refresh());
        }
    }
}