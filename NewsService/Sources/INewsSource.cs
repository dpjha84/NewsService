using NewsService.Aggregators;
using NewsService.Models;
using System.Collections.Generic;

namespace NewsService.Sources
{
    public interface INewsSource : IObservableItem
    {
        int Id { get; set; }

        string Name { get; set; }

        string Key { get; set; }

        IEnumerable<News> News { get; }

        void AddNews(IEnumerable<News> news);
    }

    public interface IObservableItem
    {
        void Attach(INewsSourceObserver observer);

        void Detach(INewsSourceObserver observer);

        void Notify();
    }
}