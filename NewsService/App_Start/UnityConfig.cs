using NewsService.Aggregators;
using NewsService.Controllers;
using NewsService.Paging;
using NewsService.Sources;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace NewsService
{
    /// <summary>
    /// Container to register all dependencies
    /// </summary>
    public static class UnityConfig
    {
        /// <summary>
        /// Register Components
        /// </summary>
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<INewsSourceRegistry<NewsSource>, NewsSourceRegistry<NewsSource>>(TypeLifetime.Singleton);
            container.RegisterType<ICrossSourceAggregationStrategy, PriorityNewsFirstAggregationStrategy>(TypeLifetime.Singleton);
            container.RegisterType<IPagingStrategy, PriorityNewsFirstWithWeightedNewsPagingStrategy>(TypeLifetime.Singleton);
            container.RegisterType<IPagedNewsAggregator, PagedNewsAggregator>(TypeLifetime.Singleton);
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}