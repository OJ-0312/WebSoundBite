using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebscraperApp
{
    public static class OpenAIProcessing
    {
        public static string GPTPrompt = "Summarize the article and only include the important parts: ";
        public static string? text;
        public static string? ArticleContent;
        //public List<string>? ResponseList;
        public static async Task<string> ChatGPTAsync(string ArticleContent)
        {
            List<string> ResponseList = new List<string>();
            string APIKey = new APIKeyHider().openAIAPIKey; //Extracting API Key from APIKeyHider.cs
            var GPTapi = new OpenAI_API.OpenAIAPI(APIKey);      //Creating a new instance of the OpenAI API
            var chat = GPTapi.Chat.CreateConversation();        //Creating a new chatgpt conversation

            chat.AppendSystemMessage("Your job today is to summarize articles and make them more easily digestable. Try to write them into around 100-200 words");
            //chatgpt instruction
            if (ArticleContent.Length > 3000)
            {
                chat.AppendUserInput("Please summarize this article" + ArticleContent.Substring(0, 3000));
            }
            else
            {
                chat.AppendUserInput("Please summarize this article" + ArticleContent);
            }
            //chatgpt instruction + substringed article (first 3000 characters) from the guardian. 
            // 3000 characters is the maximum amount of characters that can be processed by the chatgpt api

            string response = await chat.GetResponseFromChatbotAsync();
            return response;
        }
    }
}