using NewsService.Models;
using System.Collections.Generic;

namespace NewsService.Paging
{
    public interface IPagingStrategy
    {
        int PageSize { get; set; }

        IEnumerable<News> Arrange(IEnumerable<News> newsList);
    }
}