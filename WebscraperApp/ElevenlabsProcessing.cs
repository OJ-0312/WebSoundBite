using ElevenLabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WebscraperApp
{
    public class ElevenlabsProcessing
    {

        public static async Task<string> ElevenlabsProcesser(string summarizedText)
        {
            string apiKey = new APIKeyHider().GetAPIKeys().Item1;
            var api = new ElevenLabsClient(new ElevenLabsAuthentication(apiKey));
            var voice = (await api.VoicesEndpoint.GetAllVoicesAsync()).FirstOrDefault();
            var defaultVoiceSettings = await api.VoicesEndpoint.GetDefaultVoiceSettingsAsync();
            var clipPath = await api.TextToSpeechEndpoint.TextToSpeechAsync(summarizedText, voice, defaultVoiceSettings);
            return clipPath;
        }


    }
}
