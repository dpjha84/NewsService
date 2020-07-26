using NewsService.Aggregators;
using NewsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsService.Sources
{
    public class NewsSource : INewsSource
    {
        private readonly List<INewsSourceObserver> _observers = new List<INewsSourceObserver>();

        public int Id { get; set; }

        public string Key { get; set; }

        public IEnumerable<News> News { get; private set; } = new List<News>();


        public void AddNews(IEnumerable<News> news)
        {
            News = News.Concat(news);
            Notify();
        }

        public void Attach(INewsSourceObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(INewsSourceObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            _observers.ForEach(o => o.Refresh());
        }
    }
}