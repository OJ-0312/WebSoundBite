using ElevenLabs;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebscraperApp
{
    public class WebScraper
    {
        private static string finalArticleCount;
        public static int chatGPTMaxInput = 3000;
        public static async Task<List<string>> URLScraper(int themeNumber)
        {

            List<string> articleURLList = new List<string>();

            List<string> urlList = new List<string> {
            "https://www.theguardian.com/technology/artificialintelligenceai",
            "https://www.theguardian.com/business/fooddrinks",
            "https://www.theguardian.com/world/china",
            "https://www.theguardian.com/environment/renewableenergy" };

            // Find URL of the articles
            using (var httpClient = new HttpClient())
            {
                string url = urlList[themeNumber];                                     //Picks URL from UI
                var html = await Task.Run(() => httpClient.GetStringAsync(url).Result);                       //Getting the HTML code from the URL
                var htmlDocument = new HtmlDocument();                                  //Defining the HTML document
                htmlDocument.LoadHtml(html);                                            //Loading the HTML code into the document
                var articleElements = htmlDocument.DocumentNode.SelectNodes("//a");     //Get the Article Nodes of the category page


                foreach (var articleElement in articleElements)
                {
                    if (articleElement.Attributes["class"] != null && articleElement.Attributes["class"].Value == "u-faux-block-link__overlay js-headline-text")
                    {
                        articleURLList.Add(articleElement.Attributes["href"].Value);
                    }
                }
            }
            return articleURLList;
        }

        public static async Task<List<string>> URLFilter(List<string> articleURLList)

        {
            string Date = new DateFormatter().GetDateFormatter();

            List<string> articleURLFilteredList = new List<string>();

            foreach (var articleURL in articleURLList)
            {
                if (articleURL.Contains(Date) && !articleURL.Contains("video") && !articleURL.Contains("Live") && !articleURL.Contains("audio") && !articleURL.Contains("gallery"))
                {
                    articleURLFilteredList.Add(articleURL);
                }

            }

            if (articleURLFilteredList.Count == 0)
            {
                //append articleurl filted list with the first articleurl list item
                articleURLFilteredList.Insert(0, (articleURLList[0]));
                Console.WriteLine("Testorino in chatteroni");
            }

            return articleURLFilteredList;

        }
        public static async Task<List<string>> ArticleScraper(List<string> articleURLFilteredList)
        {

            string? articleContent;
            string articleXPath = "/html/body/main/article/div/div/div[6]/div/div[1]/div";


            List<string> responses = new List<string>();              //Just defining a few lists for later use
            List<string> articleContentList = new List<string>();

            using (var httpClientNew = new HttpClient())
            {

                for (int i = 0; i < articleURLFilteredList.Count; i++)
                {
                    var urlNew = articleURLFilteredList[i];
                    var htmlNew = await Task.Run(() => httpClientNew.GetStringAsync(urlNew).Result);
                    var htmlDocumentNew = new HtmlDocument();
                    htmlDocumentNew.LoadHtml(htmlNew);
                    var articleElementsNew = htmlDocumentNew.DocumentNode.SelectNodes(articleXPath);
                    if (articleElementsNew == null)
                    {
                        //error exception
                        Console.WriteLine("Error");
                    }
                    //This is the part where I need to get the text from the article
                    //After finding the filtered list of URLs.

                    for (int k = 0; k < articleElementsNew.Count; k++)
                    {
                        int articleNumber = k + 1;
                        articleContent = articleElementsNew[k].InnerText;
                        if (articleContent.Length >= chatGPTMaxInput)
                        {
                            articleContent = articleContent.Substring(0, chatGPTMaxInput);
                        }
                        articleContentList.Add(articleContent);
                    }

                }
                return (articleContentList);

            }
        }

        public static async Task<List<string>> WebScrape (int themeNumber)
        {
            List<string> urlScraperResult = new List<string>();
            List<string> urlFilterResult = new List<string>();
            List<string> articleScraperResult = new List<string>();

            urlScraperResult = await URLScraper(themeNumber);
            urlFilterResult = await URLFilter(urlScraperResult);
            articleScraperResult = await ArticleScraper(urlFilterResult);

            return articleScraperResult;
        }
        /*
                public static async Task<(string, string, string, List<string>)> WebScrape(int themeNumber)
                {

                    string? articleContent;
                    string articleXPath = "/html/body/main/article/div/div/div[6]/div/div[1]/div";


                    List<string> responses = new List<string>();              //Just defining a few lists for later use
                    List<string> articleURLList = new List<string>();
                    List<string> articleURLFilteredList = new List<string>();
                    List<string> articleContentList = new List<string>();


                    string apiKey = new APIKeyHider().elevenlabsAPIKey;
                    string Date = new DateFormatter().GetDateFormatter();


                    List<string> urlList = new List<string> {
                    "https://www.theguardian.com/technology/artificialintelligenceai",
                    "https://www.theguardian.com/business/fooddrinks",
                    "https://www.theguardian.com/world/china",
                    "https://www.theguardian.com/environment/renewableenergy" };


                    // Find URL of the articles
                    using (var httpClient = new HttpClient())
                    {
                        string url = urlList[themeNumber];                                     //Picks URL from UI
                        var html = await Task.Run(() => httpClient.GetStringAsync(url).Result);                       //Getting the HTML code from the URL
                        var htmlDocument = new HtmlDocument();                                  //Defining the HTML document
                        htmlDocument.LoadHtml(html);                                            //Loading the HTML code into the document
                        var articleElements = htmlDocument.DocumentNode.SelectNodes("//a");     //Get the Article Nodes of the category page


                        foreach (var articleElement in articleElements)
                        {
                            if (articleElement.Attributes["class"] != null && articleElement.Attributes["class"].Value == "u-faux-block-link__overlay js-headline-text")
                            {
                                articleURLList.Add(articleElement.Attributes["href"].Value);
                            }
                        }



                        foreach (var articleURL in articleURLList)
                        {
                            if (articleURL.Contains(Date) && !articleURL.Contains("video") && !articleURL.Contains("Live") && !articleURL.Contains("audio") && !articleURL.Contains("gallery"))
                            {
                                articleURLFilteredList.Add(articleURL);
                            }

                        }

                        if (articleURLFilteredList.Count == 0)
                        {
                            //append articleurl filted list with the first articleurl list item
                            articleURLFilteredList.Insert(0, (articleURLList[0]));
                            Console.WriteLine("Testorino in chatteroni");
                        }
                    }
                    using (var httpClientNew = new HttpClient())
                    {

                        for (int i = 0; i < articleURLFilteredList.Count; i++)
                        {
                            var urlNew = articleURLFilteredList[i];
                            var htmlNew = await Task.Run(() => httpClientNew.GetStringAsync(urlNew).Result);
                            var htmlDocumentNew = new HtmlDocument();
                            htmlDocumentNew.LoadHtml(htmlNew);
                            var articleElementsNew = htmlDocumentNew.DocumentNode.SelectNodes(articleXPath);
                            if (articleElementsNew == null)
                            {
                                //error exception
                                Console.WriteLine("Error");
                            }
                            //This is the part where I need to get the text from the article
                            //After finding the filtered list of URLs.
                            finalArticleCount = articleElementsNew.Count.ToString();

                            for (int k = 0; k < articleElementsNew.Count; k++)
                            {
                                int articleNumber = k + 1;
                                articleContent = articleElementsNew[k].InnerText;
                                if (articleContent.Length >= chatGPTMaxInput)
                                {
                                    articleContent = articleContent.Substring(0, chatGPTMaxInput);
                                }
                                articleContentList.Add(articleContent);
                            }

                        }
                        return (articleURLFilteredList[0], articleURLList[0], finalArticleCount, articleContentList);

                    }
                }
            */
    }
}
