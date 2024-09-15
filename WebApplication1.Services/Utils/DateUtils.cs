using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Services.Utils
{
    public static class DateUtils
    {
        public static DateTime LocalDateTime => DateTime.UtcNow.AddHours(2);

        /// <summary>
        /// Returns collection of dates within a date range
        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime to)
        {
            for (DateTime day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
                yield return day;
        }

        /// <summary>
        /// Calculates the difference between two dates and returns the duration in hours or days.
        /// </summary>
        /// <param name="startDate">The starting date.</param>
        /// <param name="endDate">The ending date.</param>
        /// <returns>A string representation of the duration between the two dates.</returns>
        public static string GetDuration(DateTime startDate, DateTime endDate)
        {
            TimeSpan difference = endDate - startDate;

            if (difference.TotalHours < 24)
            {
                double hours = Math.Round(difference.TotalHours, 1);
                return $"{hours} hours";
            }
            else
            {
                int days = (int)difference.TotalDays;
                return $"{days} days";
            }
        }

        public static DateTime GetFirstDateOfTheMonth()
        {
            DateTime currentDate = LocalDateTime;
            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            return firstDayOfMonth.Date;
        }

        public static DateTime GetLastDateOfTheMonth()
        {
            DateTime firstDayOfMonth = GetFirstDateOfTheMonth();
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            return lastDayOfMonth.Date;
        }

        /// <summary>
        /// Converts provided period to a string
        /// </summary>
        /// <returns></returns>
        public static string ToPeriodString(DateTime fromDate, DateTime toDate)
        {
            return $"{fromDate:dd/MM/yyyy} - {toDate:dd/MM/yyyy}";
        }

        public static bool DateRangesOverlap(DateTime fromDate1, DateTime toDate1, DateTime fromDate2, DateTime toDate2)
        {
            return fromDate1 <= toDate2 && fromDate2 <= toDate1;
        }

        public static DateTime PickMinDate(DateTime date1, DateTime date2)
        {
            List<DateTime> dates = new List<DateTime> { date1, date2 };
            return dates.Min();
        }

        public static DateTime PickMaxDate(DateTime date1, DateTime date2)
        {
            List<DateTime> dates = new List<DateTime> { date1, date2 };
            return dates.Max();
        }

    }
}
