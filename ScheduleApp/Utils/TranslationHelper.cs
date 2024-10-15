using System.Globalization;
using System.Resources;
using System.Threading;

namespace ScheduleApp.Utils
{
    public static class TranslationHelper
    {
        public static string GetTranslation(string key)
        {
            ResourceManager rm = new ResourceManager("ScheduleApp.Resources.LoginMessages", typeof(TranslationHelper).Assembly);
            return rm.GetString(key);
        }

        public static void SetCultureForLocation(string location)
        {
            if (location.Contains("United States"))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            }
            else if (location.Contains("Mexico"))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("es");
            }
            else
            {

                // default to english
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            }
        }

        public static void SetCultureManually(string cultureCode)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);
        }
    }
}