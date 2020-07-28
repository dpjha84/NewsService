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
    public class AggregationStrategyTests
    {
        /// <summary>
        /// Verify news aggregation.
        /// </summary>
        /// <param name="expected">Expected comma separated news list on left side below</param>
        /// <param name="input">actual comma separated news list collection on right side below</param>
        [TestCase("N1, N2, N3, N4, N5, N6",     "N1, N2, N3", "N4, N5, N6")]  
        [TestCase("P1, P2, P3, N1, N2, A1",     "N1, P1, P2", "P3, A1, N2")]  
        [TestCase("P1, P2, N1, N2, N3",         "P1, N1, P2, N2, N3", "")]    
        [TestCase("P1, P2, N1, N2, A1, A2",     "N1, P1", "A1, P2", "N2, A2")]
        [TestCase("",                           "", "")]                      
        [TestCase("P1, P2, N1, N2, A1, A2",     "P1, P2", "N1, N2", "A1, A2")]
        [TestCase("N1, N2, A1, A2", "A1, A2",   "N1, N2")]                    
        public void Verify_Aggregation(string expected, params string[] input)
        {
            var newsSourceList = new List<INewsSource>();
            foreach (var newsList in input)
            {
                var source = new Mock<INewsSource>();
                source.Setup(x => x.News)
                    .Returns(Helper.GetNews(newsList));
                newsSourceList.Add(source.Object);
            }
            var strategy = new PriorityNewsFirstAggregationStrategy();
            var actualNews = strategy.Aggregate(newsSourceList);
            Helper.ListEqualAssert(Helper.GetHeadings(expected), actualNews.Select(x => x.Heading).ToArray());
        }        
    }
}
