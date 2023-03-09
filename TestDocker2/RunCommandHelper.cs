using System.Diagnostics;

namespace TestDocker2
{
    public static class RunCommandHelper
    {
        public static async Task<object> RunCommand(string command)
        {

            using var process = new Process();
            process.StartInfo.FileName = "/bin/sh";
            process.StartInfo.Arguments = $"-c \"{command}\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            await process.WaitForExitAsync().ConfigureAwait(false);

            return new
            {
                Output = process.StandardOutput.ReadToEnd(),
                Err = process.StandardError.ReadToEnd(),
            };
        }
    }
}
