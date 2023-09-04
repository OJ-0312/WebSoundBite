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
        /*
         * This test checks if the URLScraper method returns a list of strings
         * The strings are URLs that are scraped from the website. 
         * Initially it checks a list of all the articles on the website.
         * Then later it checks the filtered list that only include articles that are from the last 24 hours.
         * as well as articles that are relevant to the theme that the user has chosen.
         */
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
        /*
         * If the earlier test succeeds, then the next step is to check if the entire function works
         * It should succesfully scrape the website, filter the articles and then scrape the content of the articles.
         */
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