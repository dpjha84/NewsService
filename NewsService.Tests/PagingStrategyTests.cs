using System;
using System.Collections.Generic;
using System.Linq;
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
    public class PagingStrategyTests
    {
        //Mock<INewsSourceRegistry<NewsSource>> registry;
        IPagedNewsAggregator aggregator;
        public PagingStrategyTests()
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
        public void Verify_Paging_Without_Ads_Returns_All_News()
        {
            var newsList = new List<News> {
                    new News { Heading = "N1" },
                    new News { Heading = "N2" },
                    new News { Heading = "N3" },
                    new News { Heading = "N4" },
                    new News { Heading = "N5" }
                };
            var pagingStrategy = new PriorityNewsFirstWithWeightedNewsPagingStrategy();
            pagingStrategy.PageSize = 4;
            var data = pagingStrategy.Arrange(newsList).ToList();
            
            Assert.AreEqual(data.Count, 5);
            Assert.AreEqual(data[0].Heading, "N1");
            Assert.AreEqual(data[1].Heading, "N2");
            Assert.AreEqual(data[2].Heading, "N3");
            Assert.AreEqual(data[3].Heading, "N4");
            Assert.AreEqual(data[4].Heading, "N5");
        }

        [TestMethod]
        public void Verify_Paging_With_Normal_News_And_Ads_Returns_Normal_News_And_Ads()
        {
            var newsList = new List<News> {
                    new News { Heading = "N1" },
                    new News { Heading = "N2" },
                    new News { Heading = "N3" },
                    new News { Heading = "A1", Category = NewsCategory.Advertisements },
                    new News { Heading = "A2", Category = NewsCategory.Advertisements }
                };
            var pagingStrategy = new PriorityNewsFirstWithWeightedNewsPagingStrategy();
            pagingStrategy.PageSize = 4;
            var data = pagingStrategy.Arrange(newsList).ToList();

            Assert.AreEqual(data.Count, 4);
            Assert.AreEqual(data[0].Heading, "N1");
            Assert.AreEqual(data[1].Heading, "N2");
            Assert.AreEqual(data[2].Heading, "N3");
            Assert.AreEqual(data[3].Heading, "A1");
        }

        [TestMethod]
        public void Verify_Paging_With_PriorityNews_No_Ads_Shows_Priority_News_First()
        {
            var newsList = new List<News> {
                    new News { Heading = "P1", IsPriority = true },
                    new News { Heading = "N1" },
                    new News { Heading = "P2", IsPriority = true },
                    new News { Heading = "N2" },
                    new News { Heading = "N3" }
                };
            var pagingStrategy = new PriorityNewsFirstWithWeightedNewsPagingStrategy();
            pagingStrategy.PageSize = 4;
            var data = pagingStrategy.Arrange(newsList).ToList();

            Assert.AreEqual(data.Count, 5);
            Assert.AreEqual(data[0].Heading, "P1");
            Assert.AreEqual(data[1].Heading, "P2");
            Assert.AreEqual(data[2].Heading, "N1");
            Assert.AreEqual(data[3].Heading, "N2");
            Assert.AreEqual(data[4].Heading, "N3");
        }

        [TestMethod]
        public void Verify_Paging_With_All_PriorityNews_Returns_All_News()
        {
            var newsList = new List<News> {
                    new News { Heading = "P1", IsPriority = true },
                    new News { Heading = "P2", IsPriority = true },
                    new News { Heading = "P3", IsPriority = true },
                    new News { Heading = "P4", IsPriority = true },
                    new News { Heading = "P5", IsPriority = true }
                };
            var pagingStrategy = new PriorityNewsFirstWithWeightedNewsPagingStrategy();
            pagingStrategy.PageSize = 4;
            var data = pagingStrategy.Arrange(newsList).ToList();

            Assert.AreEqual(data.Count, 5);
            Assert.AreEqual(data[0].Heading, "P1");
            Assert.AreEqual(data[1].Heading, "P2");
            Assert.AreEqual(data[2].Heading, "P3");
            Assert.AreEqual(data[3].Heading, "P4");
            Assert.AreEqual(data[4].Heading, "P5");
        }

        [TestMethod]
        public void Verify_Paging_With_All_Ads_Returns_No_News()
        {
            var newsList = new List<News> {
                    new News { Heading = "A1", Category = NewsCategory.Advertisements },
                    new News { Heading = "A2", Category = NewsCategory.Advertisements },
                    new News { Heading = "A3", Category = NewsCategory.Advertisements },
                    new News { Heading = "A4", Category = NewsCategory.Advertisements },
                    new News { Heading = "A5", Category = NewsCategory.Advertisements }
                };
            var pagingStrategy = new PriorityNewsFirstWithWeightedNewsPagingStrategy();
            pagingStrategy.PageSize = 4;
            var data = pagingStrategy.Arrange(newsList).ToList();

            Assert.AreEqual(data.Count, 0);
        }
    }
}
