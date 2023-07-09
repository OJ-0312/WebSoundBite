//Boilerplate
using HtmlAgilityPack;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using static System.Net.Mime.MediaTypeNames;
using ElevenLabs;



namespace WebscraperApp
{

    class Interface
    {
        static async Task Main(string[] args)
        {

            string? theme;
            int themeNumber;
            

            Console.WriteLine("Welcome to the Web Scraper App!              \n" +
                                "What are you interested in?                \n" +
                                "1. AI                                      \n" +
                                "2. Food and drink industry                 \n" +
                                "3. China                                   \n" +
                                "4. Renewable Energy");

            switch (Console.ReadLine())
            {
                case "1":
                    theme = "AI";
                    themeNumber = 0;
                    break;
                case "2":
                    theme = "Food and drink industry";
                    themeNumber = 1;
                    break;
                case "3":
                    theme = "China";
                    themeNumber = 2;
                    break;
                case "4":
                    theme = "Renewable Energy";
                    themeNumber = 3;
                    break;
                default:
                    Console.WriteLine("You have chosen an invalid option");
                    return;
            }

            var result = await WebScraper.WebScrape(themeNumber);
            List<string> articleContentList = result;
            string chatGPTAnswer;
            string clipPath;
            List<string> chatGPTAnswerList = new List<string>();

            for (int i = 0; i < articleContentList.Count; i++)
            {
                chatGPTAnswer = await OpenAIProcessing.OpenAIProcesser(articleContentList[i]);
                chatGPTAnswerList.Add(chatGPTAnswer);                
            }
            
            for (int i = 0; i < chatGPTAnswerList.Count; i++)
            {
                clipPath = await ElevenlabsProcessing.ElevenlabsProcesser(chatGPTAnswerList[i]);
                Console.WriteLine(clipPath);
            }
        }

    }
}