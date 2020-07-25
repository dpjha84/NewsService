using NewsService.Paging;
using NewsService.Sources;

namespace NewsService.Aggregators
{
    public interface INewsAggregator : INewsSourceAdder, IPagedData
    {

    }
}