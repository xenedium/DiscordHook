using System.Threading.Tasks;
using static DiscordHook.Alerts;

namespace DiscordHook
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            const string dshook = "https://discord.com/api/webhooks/849806948882055228/5STEFuPPtwaRVlfp-8hj_ynX44cgal7mYcOCaN9_bwjVBz-LdXLtKrNz4PtvOeyiuR9t";

            await SendHookAlertAsync(await GetIpAsync(), dshook);

        }
    }
}