using NewsService.Aggregators;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace NewsService
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<ICrossSourceAggregationStrategy, PriorityNewsFirstAggregationStrategy>(TypeLifetime.Singleton);
            container.RegisterType<INewsAggregator, NewsAggregator>(TypeLifetime.Singleton);
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}