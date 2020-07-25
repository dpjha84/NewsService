using NewsService.Aggregators;

namespace NewsService.Sources
{
    public interface INewsSourceAdder
    {
        bool IsSourceRegistered(string key);

        bool IsSourceRegistered(int id);

        INewsAggregator Add(INewsSource source);

        void Unregister(string sourceKey);

        INewsSource GetSource(int id);

        INewsSource GetSource(string key);
    }
}