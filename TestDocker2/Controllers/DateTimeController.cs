using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace TestDocker2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DateTimeController : ControllerBase
    {
        private const string IsoDateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ";

        public DateTimeController()
        {
        }

        [HttpPost("date-time")]
        public async Task<object> SetTime(string dateTimeValue)
        {
            return await RunCommandHelper.RunCommand($"date -s {dateTimeValue}");
        }

        [HttpGet("date-time")]
        public string GetTime()
        {
            return DateTimeOffset.UtcNow.ToString(IsoDateTimeFormat);
        }

        [HttpPost("time-zone")]
        public object SetTimeZone(string timezone)
        {
            var echo = RunCommandHelper.RunCommand($"echo {timezone} > /etc/timezone");
            var cat = RunCommandHelper.RunCommand($"cat /user/share/zoneinfo/{timezone} > /etc/timezone");

            return Task.WhenAll(echo, cat);
        }

        [HttpGet("time-zone")]
        public string GetTimeZone()
        {
            return TimeZoneInfo.Local.DisplayName;
        }
    }
}