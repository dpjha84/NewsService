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
    [TestFixture]
    public class NewsAggregatorTests
    {
        readonly IPagedNewsAggregator aggregator;
        public NewsAggregatorTests()
        {
            var registry = new Mock<INewsSourceRegistry<NewsSource>>();
            var aggregationStrategy = new Mock<ICrossSourceAggregationStrategy>();
            aggregationStrategy.Setup(x => x.Aggregate(It.IsAny<IEnumerable<INewsSource>>()))
                .Returns(Helper.GetNews("N1, N2, N3, N4, N5"));
            
            var pagingStrategy = new Mock<IPagingStrategy>();
            pagingStrategy.Setup(x => x.Arrange(It.IsAny<IEnumerable<News>>()))
                .Returns(Helper.GetNews("N1, N2, N3, N4, N5"));
            aggregator = new PagedNewsAggregator(registry.Object, aggregationStrategy.Object, pagingStrategy.Object);
        }

        [Test]
        public void Verify_Aggregated_Data_Is_Paged_Correctly()
        {
            var paging = new PagingInfo { pageNumber = 1, pageSize = 3 };
            var actual = aggregator.GetPagedData<News>(paging);
            Helper.ListEqualAssert(Helper.GetHeadings("N1, N2, N3"), actual.Select(x => x.Heading).ToArray());
            
            paging.pageNumber = 2;
            actual = aggregator.GetPagedData<News>(paging);
            Helper.ListEqualAssert(Helper.GetHeadings("N4, N5"), actual.Select(x => x.Heading).ToArray());
        }
    }
}
