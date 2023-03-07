using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace TestDocker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Get")]
        public async Task<string> Get()
        {
            using var process = new Process();
            process.StartInfo.FileName = "/bin/bash";
            process.StartInfo.Arguments = "/c ls -r *.cs";
            //process.StartInfo.FileName = "cmd";
            //process.StartInfo.Arguments = "/C dir *.cs /s";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            await process.WaitForExitAsync().ConfigureAwait(false);

            return process.StandardOutput.ReadToEnd();
        }
    }
}