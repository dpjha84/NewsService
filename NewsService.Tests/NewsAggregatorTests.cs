using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NewsService.Aggregators;
using NewsService.Controllers;
using NewsService.Models;
using NewsService.Paging;
using NewsService.Sources;
using NUnit.Framework;

namespace NewsService.Tests
{
    /// <summary>
    /// Fixture class to test all aggregation happening with pagination
    /// </summary>
    [TestFixture]
    public class NewsAggregatorTests
    {
        readonly IPagedNewsAggregator aggregator;
        const string commaSeparatedNews = "N1, N2, N3, N4, N5";

        public NewsAggregatorTests()
        {
            var registry = new Mock<INewsSourceRegistry<NewsSource>>();
            var aggregationStrategy = new Mock<ICrossSourceAggregationStrategy>();
            aggregationStrategy.Setup(x => x.Aggregate(It.IsAny<IEnumerable<INewsSource>>()))
                .Returns(Helper.GetNews(commaSeparatedNews));
            
            var pagingStrategy = new Mock<IPagingStrategy>();
            pagingStrategy.Setup(x => x.Arrange(It.IsAny<IEnumerable<News>>()))
                .Returns(Helper.GetNews(commaSeparatedNews));
            aggregator = new PagedNewsAggregator(registry.Object, aggregationStrategy.Object, pagingStrategy.Object);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(8)]
        public void Verify_Aggregated_Data_Is_Paged_Correctly(int size)
        {
            var paging = new PagingInfo { pageNumber = 1, pageSize = size };
            var actual = aggregator.GetPagedData<News>(paging.pageNumber, paging.pageSize);
            Helper.ListEqualAssert(Helper.GetHeadings(commaSeparatedNews).Take(size).ToArray(), actual.Select(x => x.Heading).ToArray());
            
            paging.pageNumber = 2;
            actual = aggregator.GetPagedData<News>(paging.pageNumber, paging.pageSize);
            Helper.ListEqualAssert(Helper.GetHeadings(commaSeparatedNews).Skip(size).Take(size).ToArray(), actual.Select(x => x.Heading).ToArray());
        }
    }
}
