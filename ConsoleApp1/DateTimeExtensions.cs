using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class DateTimeExtensions
    {
        public static DateTime LastDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        public static decimal GetDaysDifference(DateTime startDate, DateTime endDate)
        {
            return (decimal)(endDate - startDate).TotalDays;
        }

        public static decimal GetWeeksDifference(DateTime startDate, DateTime endDate, bool roundUp)
        {
            if (roundUp)
                return (decimal)Math.Ceiling((endDate - startDate).TotalDays / 7.0);
            else
                return (decimal)Math.Floor((endDate - startDate).TotalDays / 7.0);
        }
        public static decimal GetMonthsDifference(DateTime startDate, DateTime endDate)
        {
            return (endDate.Year - startDate.Year) * 12 + (endDate.Month - startDate.Month);
        }

        public static bool IsLeapYear(int year)
        {
            return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
        }

        public static int Days360(DateTime startDate, DateTime endDate, bool europeanMethod = true)
        {
            int startDay = startDate.Day;
            int startMonth = startDate.Month;
            int startYear = startDate.Year;

            int endDay = endDate.Day;
            int endMonth = endDate.Month;
            int endYear = endDate.Year;

            if (!europeanMethod)
            {
                // US method adjustments
                if (startDay == 31)
                {
                    startDay = 30;
                }
                if (endDay == 31)
                {
                    endDay = 30;
                }
            }
            else
            {
                // European method adjustments
                if (startDay == 31) startDay = 30;
                if (endDay == 31) endDay = 30;
            }

            // Handle specific case: Full year (1/1 to 12/31)
            if (startDate.Month == 1 && startDate.Day == 1 && endDate.Month == 12 && endDate.Day == 31)
            {
                return 360; // Full 360-day year
            }

            if (IsLeapYear(startYear) && startMonth == 2 && startDay == 29)
            {

            }
            else if (!IsLeapYear(startYear) && startMonth == 2 && startDay == 28)
            {
                endDay -= 1;
            }
            else if (IsLeapYear(endYear) && endMonth == 2 && endDay == 29)
            {
                endDay += 2;
            }
            else if (!IsLeapYear(endYear) && endMonth == 2 && endDay == 28)
            {
                endDay += 3;
            }
            else
            {
                endDay += 1;
            }


            // General calculation
            return (endYear - startYear) * 360 + (endMonth - startMonth) * 30 + (endDay - startDay);
        }


    }
}
