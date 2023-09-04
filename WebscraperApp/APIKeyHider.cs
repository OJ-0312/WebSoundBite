using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WebscraperApp
{
    public class APIKeyHider
    {
        public string elevenlabsAPIKey;
        public string openAIAPIKey;
        /* This method reads the API keys from a text file and returns them as a tuple.
         * It is a primitive way of hiding the API keys.
         * Better ways of hiding the API keys are available, but this is the simplest way. 
         * There are some solutions suggested using a proxy in the github of the openAI C# github
         */
        public (string, string) GetAPIKeys()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/APIKeys/ApiKeys.txt";
            //read text file containing api keys
            StreamReader sr = new StreamReader(path);
            string line = sr.ReadLine();
            while (line != null)
            {
                if (line.Contains("elevenlabsAPIKey"))
                {
                    elevenlabsAPIKey = line.Substring(33);
                }
                else if (line.Contains("openAIAPIKey"))
                {
                    openAIAPIKey = line.Substring(29);
                }
                line = sr.ReadLine();
            }
            sr.Close();
            return (elevenlabsAPIKey, openAIAPIKey);
        }
    }
}
