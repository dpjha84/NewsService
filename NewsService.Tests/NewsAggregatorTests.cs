using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NewsService.Aggregators;
using NewsService.Controllers;
using NewsService.Models;
using NewsService.Paging;
using NewsService.Sources;

namespace NewsService.Tests
{
    [TestClass]
    public class NewsAggregatorTests
    {
        //Mock<INewsSourceRegistry<NewsSource>> registry;
        IPagedNewsAggregator aggregator;
        public NewsAggregatorTests()
        {
            var registry = new Mock<INewsSourceRegistry<NewsSource>>();
            //var source1 = new NewsSource();
            //var source2 = new NewsSource();
            //source1.AddNews(new List<News> { new News { Heading = "H1" }, new News { Heading = "H2" } });
            //source1.AddNews(new List<News> { new News { Heading = "H3" }, new News { Heading = "H4" } });
            //registry.Setup(x => x.GetNewsSources())
            //    .Returns(new List<NewsSource> { new NewsSource { Key = "S1" }, new NewsSource { Key = "S2" } });
            var aggregationStrategy = new Mock<ICrossSourceAggregationStrategy>();
            aggregationStrategy.Setup(x => x.Aggregate(It.IsAny<IEnumerable<INewsSource>>()))
                .Returns(new List<News> { 
                    new News { Heading = "N1" }, 
                    new News { Heading = "N2" },
                    new News { Heading = "N3" },
                    new News { Heading = "N4" },
                    new News { Heading = "N5" }
                });
            
            var pagingStrategy = new Mock<IPagingStrategy>();
            pagingStrategy.Setup(x => x.Arrange(It.IsAny<IEnumerable<News>>()))
                .Returns(new List<News> {
                    new News { Heading = "N1" },
                    new News { Heading = "N2" },
                    new News { Heading = "N3" },
                    new News { Heading = "N4" },
                    new News { Heading = "N5" }
                });
            aggregator = new PagedNewsAggregator(registry.Object, aggregationStrategy.Object, pagingStrategy.Object);
        }

        [TestMethod]
        public void Verify_Aggregated_Data_Is_Paged_Correctly()
        {
            var paging = new PagingInfo { pageNumber = 1, pageSize = 3 };
            var data = aggregator.GetPagedData<News>(paging);
            Assert.AreEqual(data.Count, 3);
            Assert.AreEqual(data[0].Heading, "N1");
            Assert.AreEqual(data[1].Heading, "N2");
            Assert.AreEqual(data[2].Heading, "N3");

            paging.pageNumber = 2;
            data = aggregator.GetPagedData<News>(paging);
            Assert.AreEqual(data.Count, 2);
            Assert.AreEqual(data[0].Heading, "N4");
            Assert.AreEqual(data[1].Heading, "N5");
        }
    }
}
