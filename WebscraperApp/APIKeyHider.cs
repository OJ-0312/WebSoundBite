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
                    elevenlabsAPIKey = line.Substring(34);
                }
                else if (line.Contains("openAIAPIKey"))
                {
                    openAIAPIKey = line.Substring(30);
                }
                line = sr.ReadLine();
            }
            sr.Close();
            return (elevenlabsAPIKey, openAIAPIKey);
        }
    }
}
