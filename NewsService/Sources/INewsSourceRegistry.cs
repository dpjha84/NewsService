using System;
using System.Collections.Generic;

namespace NewsService.Controllers
{
    public interface INewsSourceRegistry<T>
    {
        IEnumerable<T> GetNewsSources();

        bool IsRegistered(string sourceKey);

        bool IsRegistered(int sourceId);

        T GetSource(string sourceKey);

        T GetSource(int sourceId);

        T Register(string sourceKey);

        void Unregister(string sourceKey);

        event EventHandler<EventArgs> OnSourceRegistered;

        event EventHandler<EventArgs> OnSourceUnregistered;
    }
}