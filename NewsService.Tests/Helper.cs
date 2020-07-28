using NewsService.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsService.Tests
{
    public class Helper
    {
        /// <summary>
        /// create news list based on comma separated news headings
        /// </summary>
        /// <param name="commaSeparatedNews"></param>
        /// <returns></returns>
        public static List<News> GetNews(string commaSeparatedNews)
        {
            var headings = Array.ConvertAll(commaSeparatedNews.Split(','), p => p.Trim());
            var list = new List<News>();
            foreach (var item in headings)
            {
                if (item.StartsWith("P"))
                    list.Add(new News { Heading = item, IsPriority = true });
                else if (item.StartsWith("N"))
                    list.Add(new News { Heading = item });
                else if (item.StartsWith("A"))
                    list.Add(new News { Heading = item, Category = NewsCategory.Advertisements });
            }
            return list;
        }

        /// <summary>
        /// Compare 2 lists with news headings
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        public static void ListEqualAssert(string[] expected, string[] actual)
        {
            Assert.AreEqual(actual.Length, expected.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        /// <summary>
        /// Gets an array of news headings from comma separated data
        /// </summary>
        /// <param name="commaSeparatedHeadings"></param>
        /// <returns></returns>
        public static string[] GetHeadings(string commaSeparatedHeadings)
        {
            return Array.ConvertAll(commaSeparatedHeadings.
                Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries), p => p.Trim());
        }
    }
}
