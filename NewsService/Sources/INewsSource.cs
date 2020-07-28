using NewsService.Aggregators;
using NewsService.Models;
using System.Collections.Generic;

namespace NewsService.Sources
{
    /// <summary>
    /// Interface for news source
    /// </summary>
    public interface INewsSource : IObservableItem
    {
        int Id { get; set; }

        string Key { get; set; }

        IEnumerable<News> News { get; }

        void AddNews(IEnumerable<News> news);
    }
}