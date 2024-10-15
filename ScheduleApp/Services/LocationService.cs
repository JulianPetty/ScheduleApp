using System;

namespace ScheduleApp.Services
{
    public class LocationService
    {
        public string GetUserTimezone()
        {
            TimeZoneInfo localZone = TimeZoneInfo.Local;
            string timeZoneId = localZone.Id;

            // Map the timezone to the office locations
            if (timeZoneId.Contains("Pacific"))
            {
                return "Phoenix, Arizona";
            }
            else if (timeZoneId.Contains("Eastern"))
            {
                return "New York, New York";
            }
            else if (timeZoneId.Contains("GMT") || timeZoneId.Contains("Europe/London"))
            {
                return "London, England";
            }

            return "Unknown Location";
        }
    }
}
