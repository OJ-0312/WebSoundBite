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
        private static string test;

        public static async Task<(string, string, string,List<string>)> webScraper(int themeNumber)
        {

            string? ArticleContent;
            string ArticleXPath = "/html/body/main/article/div/div/div[6]/div/div[1]/div";
                                    

            List<string> Responses = new List<string>();              //Just defining a few lists for later use
            List<string> ArticleURL_List = new List<string>();
            List<string> ArticleURLFiltered_List = new List<string>();
            List<string> ArticleContentList = new List<string>();


            string APIKey = new APIKeyHider().elevenlabsAPIKey;
            string Date = new DateFormatter().GetDateFormatter();


            List<string> URL_List = new List<string> {
            "https://www.theguardian.com/technology/artificialintelligenceai",
            "https://www.theguardian.com/business/fooddrinks",
            "https://www.theguardian.com/world/china",
            "https://www.theguardian.com/environment/renewableenergy" };


            // Find URL of the articles
            using (var httpClient = new HttpClient())
            {
                string url = URL_List[themeNumber];                                     //Picks URL from UI
                var html = await Task.Run(() => httpClient.GetStringAsync(url).Result);                       //Getting the HTML code from the URL
                var htmlDocument = new HtmlDocument();                                  //Defining the HTML document
                htmlDocument.LoadHtml(html);                                            //Loading the HTML code into the document
                var articleElements = htmlDocument.DocumentNode.SelectNodes("//a");     //Get the Article Nodes of the category page


                foreach (var articleElement in articleElements)
                {
                    if (articleElement.Attributes["class"] != null && articleElement.Attributes["class"].Value == "u-faux-block-link__overlay js-headline-text")
                    {
                        ArticleURL_List.Add(articleElement.Attributes["href"].Value);
                    }
                }



                foreach (var articleURL in ArticleURL_List)
                {
                    if (articleURL.Contains(Date) && !articleURL.Contains("video") && !articleURL.Contains("Live"))
                    {
                        ArticleURLFiltered_List.Add(articleURL);
                    }

                }

                if (ArticleURLFiltered_List.Count == 0)
                {
                    //append articleurl filted list with the first articleurl list item
                    ArticleURLFiltered_List.Insert(0, (ArticleURL_List[0]));
                    Console.WriteLine("Testorino in chatteroni");
                }
            }
            using (var httpClientNew = new HttpClient())
            {

                for (int i = 0; i < ArticleURLFiltered_List.Count; i++)
                {
                    var URLNew = ArticleURLFiltered_List[i];
                    var htmlNew = await Task.Run(() => httpClientNew.GetStringAsync(URLNew).Result);
                    var htmlDocumentNew = new HtmlDocument();
                    htmlDocumentNew.LoadHtml(htmlNew);
                    var articleElementsNew = htmlDocumentNew.DocumentNode.SelectNodes(ArticleXPath);
                    if (articleElementsNew == null)
                    {
                        //error exception
                        Console.WriteLine("Error");
                    }
                    //This is the part where I need to get the text from the article
                    //After finding the filtered list of URLs.
                    test = articleElementsNew.Count.ToString();

                    for (int k = 0; k < articleElementsNew.Count; k++)
                    {
                        int ArticleNumber = k + 1;
                        ArticleContent = articleElementsNew[k].InnerText;
                        if (ArticleContent.Length >= 3000)
                        {
                            ArticleContent = ArticleContent.Substring(0, 3000);
                        }
                        ArticleContentList.Add(ArticleContent);
                    }

                }
                return (ArticleURLFiltered_List[0], ArticleURL_List[0], test, ArticleContentList);

            }
            //return (ArticleURLFiltered_List[0], ArticleURL_List[0], ArticleContentList);
        }

    }
}
