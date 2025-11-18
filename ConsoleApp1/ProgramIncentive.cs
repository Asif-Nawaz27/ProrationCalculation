using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class ProgramIncentive
    {
        public static void CalculateAllProration()
        {
            // Data from the attached image
            var salaryData = new[]
            {
                //new { BonusFromDate = new DateTime(2023, 4, 1), BonusToDate = new DateTime(2023, 4, 1) },
                //new { BonusFromDate = new DateTime(2023, 4, 1), BonusToDate = new DateTime(2023, 4, 2) },
                //new { BonusFromDate = new DateTime(2023, 4, 1), BonusToDate = new DateTime(2023, 4, 14) },
                //new { BonusFromDate = new DateTime(2023, 4, 1), BonusToDate = new DateTime(2023, 4, 15) },
                //new { BonusFromDate = new DateTime(2023, 4, 1), BonusToDate = new DateTime(2023, 4, 16) },
                //new { BonusFromDate = new DateTime(2023, 4, 1), BonusToDate = new DateTime(2023, 4, 29) },
                //new { BonusFromDate = new DateTime(2023, 4, 1), BonusToDate = new DateTime(2023, 4, 30) },
                //new { BonusFromDate = new DateTime(2023, 4, 1), BonusToDate = new DateTime(2023, 5, 1) },
                //new { BonusFromDate = new DateTime(2023, 4, 1), BonusToDate = new DateTime(2023, 5, 2) },
                //new { BonusFromDate = new DateTime(2023, 1, 1), BonusToDate = new DateTime(2023, 4, 30) },
                //new { BonusFromDate = new DateTime(2023, 1, 2), BonusToDate = new DateTime(2023, 4, 30) },
                //new { BonusFromDate = new DateTime(2023, 1, 14), BonusToDate = new DateTime(2023, 4, 30) },
                //new { BonusFromDate = new DateTime(2023, 1, 15), BonusToDate = new DateTime(2023, 4, 30) },
                //new { BonusFromDate = new DateTime(2023, 1, 16), BonusToDate = new DateTime(2023, 4, 30) },
                //new { BonusFromDate = new DateTime(2023, 1, 30), BonusToDate = new DateTime(2023, 4, 30) },
                //new { BonusFromDate = new DateTime(2023, 1, 31), BonusToDate = new DateTime(2023, 4, 30) },
                //new { BonusFromDate = new DateTime(2023, 2, 1), BonusToDate = new DateTime(2023, 4, 30) },
                //new { BonusFromDate = new DateTime(2023, 2, 2), BonusToDate = new DateTime(2023, 4, 30) },
                new { BonusFromDate = new DateTime(2025, 1, 1), BonusToDate = new DateTime(2025, 12, 31) },
                new { BonusFromDate = new DateTime(2025, 1, 1), BonusToDate = new DateTime(2025, 4, 14) },
            };



            Console.WriteLine("Last Salary Eff Date\tNew Salary Eff Date\tDays\tWeekDown\tWeekUp\tMonthRound\tMonthUp\tMonthDown\tDays360");
            Console.WriteLine("=".PadRight(140, '='));





            foreach (var data in salaryData)
            {
                decimal periodDays = DateTimeSalaryProration.IsLeapYear(data.BonusFromDate, data.BonusToDate) ? 366 : 365;
                decimal periodMonths = 12;
                decimal periodWeeks = 52;

                int days = DateTimeIncentiveProration.Days(data.BonusToDate, data.BonusFromDate);
                int MonthRound = DateTimeIncentiveProration.MonthRound(data.BonusFromDate, data.BonusToDate);
                int MonthUp = DateTimeIncentiveProration.MonthUp(data.BonusFromDate, data.BonusToDate);
                int MonthDown = DateTimeIncentiveProration.MonthDown(data.BonusFromDate, data.BonusToDate);
                int WeekDown = DateTimeIncentiveProration.WeekDown(data.BonusFromDate, data.BonusToDate);
                int WeekUp = DateTimeIncentiveProration.WeekUp(data.BonusFromDate, data.BonusToDate);
                int day360 = DateTimeIncentiveProration.Days360(data.BonusFromDate, data.BonusToDate);
                //bool isLeapYear = DateTimeIncentiveProration.IsLeapYear(data.BonusFromDate, data.BonusToDate);

                Console.WriteLine($"{data.BonusFromDate:M/d/yyyy}\t\t{data.BonusToDate:M/d/yyyy}\t\t{days}\t{WeekDown}\t\t{WeekUp}\t{MonthRound}\t\t{MonthUp}\t\t{MonthDown}\t\t{day360}");

                //Console.WriteLine($"{data.BonusFromDate:M/d/yyyy}\t\t{data.BonusToDate:M/d/yyyy}\t\t{days}\t{MonthRound}\t\t{MonthUp}\t{MonthDown}\t\t{WeekDown}\t\t{WeekUp}\t\t{isLeapYear}");
                //decimal daysProration = Math.Round(DateTimeSalaryProration.Days(data.BonusToDate, data.BonusFromDate) / periodDays * 100, 7);
                //Console.WriteLine($"Days proration: {daysProration}");

                //decimal MonthRoundProration = Math.Round(DateTimeSalaryProration.MonthRound(data.BonusFromDate, data.BonusToDate) / periodMonths * 100, 7);
                //Console.WriteLine($"MonthRound proration: {MonthRoundProration}");

                //Console.WriteLine("=".PadRight(140, '='));
            }
        }

    }
}
