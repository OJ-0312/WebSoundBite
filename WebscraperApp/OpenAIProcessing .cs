using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebscraperApp
{
    public static class OpenAIProcessing
    {
        public static string gptPrompt = "Summarize the article and only include the important parts: ";
        public static string? text;
        public static string? articleContent;
        public static int chatGPTMaxInput = 3000;

        //public List<string>? ResponseList;
        /* This simply takes in the article content and returns a string of the summarized article
         */
        public static async Task<string> OpenAIProcesser(string articleContent)
        {
            List<string> responseList = new List<string>();
            string apiKey = new APIKeyHider().GetAPIKeys().Item2; //Extracting API Key from APIKeyHider.cs txt file
            var gptAPI = new OpenAI_API.OpenAIAPI(apiKey);      //Creating a new instance of the OpenAI API
            var chat = gptAPI.Chat.CreateConversation();        //Creating a new chatgpt conversation

            chat.AppendSystemMessage("Your job today is to summarize articles and make them more easily digestable. Try to write them into around 100-200 words");
            //chatgpt instruction
            if (articleContent.Length > chatGPTMaxInput)
            {
                chat.AppendUserInput("Please summarize this article" + articleContent.Substring(0, chatGPTMaxInput));
            }
            else
            {
                chat.AppendUserInput("Please summarize this article" + articleContent);
            }
            //chatgpt instruction + substringed article (first 3000 characters) from the guardian. 
            // 3000 characters is the maximum amount of characters that can be processed by the chatgpt api

            string response = await chat.GetResponseFromChatbotAsync();
            return response;
        }
    }
}