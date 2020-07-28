using NewsService.Paging;
using NUnit.Framework;

namespace NewsService.Tests
{
    [TestFixture]
    public class PagingInfoTests
    {
        /// <summary>
        /// Verify Page Size is set correctly
        /// </summary>
        /// <param name="inputSize"></param>
        /// <param name="expectedSize"></param>
        [TestCase(1, 1)]
        [TestCase(5, 5)]
        [TestCase(8, 8)]
        [TestCase(10, PagingInfo.maxPageSize)]
        [TestCase(-5, PagingInfo.defaultPageSize)]
        public void Verify_PageSize(int inputSize, int expectedSize)
        {
            var paging = new PagingInfo { pageNumber = 1, pageSize = inputSize };
            Assert.AreEqual(expectedSize, paging.pageSize);
        }

        /// <summary>
        /// Verify Page number is set correctly
        /// </summary>
        /// <param name="inputSize"></param>
        /// <param name="expectedSize"></param>
        [TestCase(1, 1)]
        [TestCase(5, 5)]
        [TestCase(0, PagingInfo.defaultPageNumber)]
        [TestCase(-5, PagingInfo.defaultPageNumber)]
        public void Verify_PageNumber(int inputNumber, int expectedNumber)
        {
            var paging = new PagingInfo { pageNumber = inputNumber, pageSize = 8 };
            Assert.AreEqual(expectedNumber, paging.pageNumber);
        }
    }
}
