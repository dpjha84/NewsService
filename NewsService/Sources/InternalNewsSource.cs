using NewsService.Aggregators;
using NewsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsService.Sources
{
    public abstract class NewsSource : INewsSource
    {
        private readonly List<INewsSourceObserver> _observers = new List<INewsSourceObserver>();

        public int Id { get; set; }

        public string Name { get; set; }

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

    public class InternalNewsSource : NewsSource
    {
        //public override IEnumerable<News> News
        //{
        //    get
        //    {
        //        return _news;
        //    }
        //    set
        //    {
        //        _news = new List<News>
        //        {
        //            new News { Heading = "First Heading", Content = "First Content", IsPriority = true },
        //            new News { Heading = "Second Heading", Content = "Second Content", IsPriority = false }
        //        };
        //    }
        //}
    }
}