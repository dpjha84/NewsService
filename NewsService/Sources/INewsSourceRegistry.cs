using System;
using System.Collections.Generic;

namespace NewsService.Controllers
{
    /// <summary>
    /// Interface for News source registry
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INewsSourceRegistry<T>
    {
        /// <summary>
        /// Get all registered News Sources
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetNewsSources();

        /// <summary>
        /// Check if a source key is registered as news source
        /// </summary>
        /// <param name="sourceKey"></param>
        /// <returns></returns>
        bool IsRegistered(string sourceKey);

        /// <summary>
        /// Check if a source id is registered as news source
        /// </summary>
        /// <param name="sourceKey"></param>
        /// <returns></returns>
        bool IsRegistered(int sourceId);

        /// <summary>
        /// Get news source for given source key
        /// </summary>
        /// <param name="sourceKey"></param>
        /// <returns></returns>
        T GetSource(string sourceKey);

        /// <summary>
        /// Get news source for given source id
        /// </summary>
        /// <param name="sourceId"></param>
        /// <returns></returns>
        T GetSource(int sourceId);

        /// <summary>
        /// Register a news source with source key
        /// </summary>
        /// <param name="sourceKey"></param>
        /// <returns></returns>
        T Register(string sourceKey);

        /// <summary>
        /// Unregister a news source with source key
        /// </summary>
        /// <param name="sourceKey"></param>
        /// <returns></returns>
        void Unregister(string sourceKey);

        /// <summary>
        /// Event to be raised once news source was registered
        /// </summary>
        event EventHandler<EventArgs> OnSourceRegistered;

        /// <summary>
        /// Event to be raised once news source was unregistered
        /// </summary>
        event EventHandler<EventArgs> OnSourceUnregistered;
    }
}