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
        public async Task URLTesterAsync(int themeNumber)
        {
            bool expected = true;

            bool actual;
            List<string> urlScraperResult = new List<string>();
            List<string> urlFilterResult = new List<string>();

            urlScraperResult = await WebScraper.URLScraper(themeNumber);
            urlFilterResult = await WebScraper.URLFilter(urlScraperResult);


            if (urlScraperResult[0] != urlFilterResult[0])
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
        public async Task ArticleContentTester(int themeNumber)
        {
            bool expected = true;

            bool actual;

            var result = await WebScraper.WebScrape(themeNumber);

            if (result == null)
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