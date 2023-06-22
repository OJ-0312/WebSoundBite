using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebscraperApp.Tests
{

    public class WebscraperTest
    {
        public WebScraper? WS;

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task URLTesterAsync(int ThemeNumber)
        {
            bool expected = true;

            bool actual;
            var result = await WebScraper.webScraper(ThemeNumber);
            string item1 = result.Item1;
            string item2 = result.Item2;
            string item3 = result.Item3;
            List<string> item4 = result.Item4;

            if (item1 != item2)
            {
                actual = false;   
            }
            else
            {
                actual = true;
            }

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task ArticleContentTester(int ThemeNumber)
        {
            bool expected = true;

            bool actual;
            var result = await WebScraper.webScraper(ThemeNumber);
            List<string> item4 = result.Item4;

            if (item4 == null)
            {
                actual = false;
            }
            else
            {
                actual = true;
            }

            Assert.Equal(expected, actual);
        }

    }
}