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

        //public static List<News> GetNewsSources(params string[] commaSeparatedNewsSources)
        //{
        //    foreach (var source in commaSeparatedNewsSources)
        //    {

        //    }
        //    var newsSources = Array.ConvertAll(commaSeparatedNewsSources.Split(','), p => p.Trim());
        //    var list = new List<News>();
        //    foreach (var item in headings)
        //    {
        //        if (item.StartsWith("P"))
        //            list.Add(new News { Heading = item, IsPriority = true });
        //        else if (item.StartsWith("N"))
        //            list.Add(new News { Heading = item });
        //        else if (item.StartsWith("A"))
        //            list.Add(new News { Heading = item, Category = NewsCategory.Advertisements });
        //    }
        //    return list;
        //}

        public static void ListEqualAssert(string[] expected, string[] actual)
        {
            Assert.AreEqual(actual.Length, expected.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        public static string[] GetHeadings(string commaSeparatedHeadings)
        {
            return Array.ConvertAll(commaSeparatedHeadings.
                Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries), p => p.Trim());
        }
    }
}
