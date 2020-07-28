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
    public class PagingStrategyTests
    {
        /// <summary>
        /// Verify aggregated news is sequenced correctly as per the paging strategy.
        /// </summary>
        /// <param name="commaSepratedInput">Comma separated news as input on left side below</param>
        /// <param name="commaSepratedExpected">Comma separated page sequence as output on right side below</param>
        [TestCase("N1, N2, N3, N4, N5",     "N1, N2, N3, N4, N5")]  // All normal news
        [TestCase("N1, N2, N3, A1, A2",     "N1, N2, N3, A1")]      // Normal news and Ads
        [TestCase("P1, N1, P2, N2, N3",     "P1, P2, N1, N2, N3")]  // Priority and normal news
        [TestCase("P1, P2, P3, P4, P5",     "P1, P2, P3, P4, P5")]  // All priority news
        [TestCase("A1, A2, A3, A4, A5",     "")]                    // All Ads
        [TestCase("P1, A1, P2, P3, A2",     "P1, P2, P3, A1")]      // Priority news and Ads
        [TestCase("N1, P1, P2, N2, A1",     "P1, P2, N1, A1, N2")]  // Priority news and Ads
        public void Verify_Paging_Sequence_With_Page_Size_4(string commaSepratedInput, string commaSepratedExpected)
        {
            var actualNews = Paging.Arrange(Helper.GetNews(commaSepratedInput));
            Helper.ListEqualAssert(Helper.GetHeadings(commaSepratedExpected), actualNews.Select(x => x.Heading).ToArray());
        }

        IPagingStrategy Paging => new PriorityNewsFirstWithWeightedNewsPagingStrategy { PageSize = 4 };
    }
}
