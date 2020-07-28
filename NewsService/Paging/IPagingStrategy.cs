using NewsService.Models;
using System.Collections.Generic;

namespace NewsService.Paging
{
    /// <summary>
    /// Interface for paging strategy
    /// </summary>
    public interface IPagingStrategy
    {
        /// <summary>
        /// Page size
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// Arrange the news honoring page size and custom rules
        /// </summary>
        /// <param name="newsList"></param>
        /// <returns></returns>
        IEnumerable<News> Arrange(IEnumerable<News> newsList);
    }
}