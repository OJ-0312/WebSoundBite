using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


namespace WebscraperApp
{
    public class DateFormatter
    {
        public DateTime thisDay = DateTime.Today;
        public CultureInfo? ci;
        public DateTimeFormatInfo? dtfi;

        //Formats the date into a string that fits with the format in the HTML code
        //This allows you to extract the articles from today and not just the most recent articles

        public string GetDateFormatter()
        {
            String dayDate = thisDay.Day.ToString();
            int monthDate = thisDay.Month;
            ci = CultureInfo.CreateSpecificCulture("en-US"); // Just to make sure the date is in english
            dtfi = ci.DateTimeFormat;
            string newMonthDate = dtfi.GetMonthName(monthDate).ToLower();
            string date = newMonthDate.Substring(0,3) + "/" + dayDate; // The date is now in the format of "month/day"
            return date;
        }
    }
}
