using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DiscordHook
{
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
                    var jsonresponse = await httpclient.GetStringAsync(token);
                    
                }
                catch
                {
                    await Task.Delay(500);
                }
            }
        }  
    }
}