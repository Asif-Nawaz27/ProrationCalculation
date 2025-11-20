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
            var IncentiveData = new[]
            {
                new { BonusFromDate = new DateTime(2023, 4, 1), BonusToDate = new DateTime(2023, 4, 1) },
                new { BonusFromDate = new DateTime(2023, 4, 1), BonusToDate = new DateTime(2023, 4, 2) },
                new { BonusFromDate = new DateTime(2023, 4, 1), BonusToDate = new DateTime(2023, 4, 14) },
                new { BonusFromDate = new DateTime(2023, 4, 1), BonusToDate = new DateTime(2023, 4, 15) },
                new { BonusFromDate = new DateTime(2023, 4, 1), BonusToDate = new DateTime(2023, 4, 16) },
                new { BonusFromDate = new DateTime(2023, 4, 1), BonusToDate = new DateTime(2023, 4, 29) },
                new { BonusFromDate = new DateTime(2023, 4, 1), BonusToDate = new DateTime(2023, 4, 30) },
                new { BonusFromDate = new DateTime(2023, 4, 1), BonusToDate = new DateTime(2023, 5, 1) },
                new { BonusFromDate = new DateTime(2023, 4, 1), BonusToDate = new DateTime(2023, 5, 2) },
                new { BonusFromDate = new DateTime(2023, 1, 1), BonusToDate = new DateTime(2023, 4, 30) },
                new { BonusFromDate = new DateTime(2023, 1, 2), BonusToDate = new DateTime(2023, 4, 30) },
                new { BonusFromDate = new DateTime(2023, 1, 14), BonusToDate = new DateTime(2023, 4, 30) },
                new { BonusFromDate = new DateTime(2023, 1, 15), BonusToDate = new DateTime(2023, 4, 30) },
                new { BonusFromDate = new DateTime(2023, 1, 16), BonusToDate = new DateTime(2023, 4, 30) },
                new { BonusFromDate = new DateTime(2023, 1, 30), BonusToDate = new DateTime(2023, 4, 30) },
                new { BonusFromDate = new DateTime(2023, 1, 31), BonusToDate = new DateTime(2023, 4, 30) },
                new { BonusFromDate = new DateTime(2023, 2, 1), BonusToDate = new DateTime(2023, 4, 30) },
                new { BonusFromDate = new DateTime(2023, 2, 2), BonusToDate = new DateTime(2023, 4, 30) },
                //new { BonusFromDate = new DateTime(2025, 1, 1), BonusToDate = new DateTime(2025, 12, 31) },
                //new { BonusFromDate = new DateTime(2025, 1, 1), BonusToDate = new DateTime(2025, 4, 14) },
                //new { BonusFromDate = new DateTime(2025, 1, 1), BonusToDate = new DateTime(2025, 4, 15) },
                //new { BonusFromDate = new DateTime(2025, 1, 1), BonusToDate = new DateTime(2025, 4, 16) },
            };

            Console.WriteLine("Last Salary Eff Date\tNew Salary Eff Date\tDays\tWeekDown\tWeekUp\tMonthRound\tMonthUp\tMonthDown\tDays360");
            Console.WriteLine("=".PadRight(140, '='));

            foreach (var data in IncentiveData)
            {
                decimal days = DateTimeIncentiveProration.Days(data.BonusFromDate, data.BonusToDate);
                decimal MonthRound = DateTimeIncentiveProration.MonthRound(data.BonusFromDate, data.BonusToDate);
                decimal MonthUp = DateTimeIncentiveProration.MonthUp(data.BonusFromDate, data.BonusToDate);
                decimal MonthDown = DateTimeIncentiveProration.MonthDown(data.BonusFromDate, data.BonusToDate);
                decimal WeekDown = DateTimeIncentiveProration.WeekDown(data.BonusFromDate, data.BonusToDate);
                decimal WeekUp = DateTimeIncentiveProration.WeekUp(data.BonusFromDate, data.BonusToDate);
                decimal day360 = DateTimeIncentiveProration.Days360(data.BonusFromDate, data.BonusToDate);
                //bool isLeapYear = DateTimeIncentiveProration.IsLeapYear(data.BonusFromDate, data.BonusToDate);

                Console.WriteLine($"{data.BonusFromDate:M/d/yyyy}\t\t{data.BonusToDate:M/d/yyyy}\t\t{days}\t{WeekDown}\t\t{WeekUp}\t{MonthRound}\t\t{MonthUp}\t\t{MonthDown}\t\t{day360}");

                decimal totalDays = DateTimeIncentiveProration.Days(new DateTime(2023, 4, 1), new DateTime(2023, 4, 30));
                decimal totalMonthRound = DateTimeIncentiveProration.MonthRound(new DateTime(2023, 4, 1), new DateTime(2023, 4, 30));
                decimal totalMonthUp = DateTimeIncentiveProration.MonthUp(new DateTime(2023, 4, 1), new DateTime(2023, 4, 30));
                decimal totalMonthDown = DateTimeIncentiveProration.MonthDown(new DateTime(2023, 4, 1), new DateTime(2023, 4, 30));
                decimal totalWeekDown = DateTimeIncentiveProration.WeekDown(new DateTime(2023, 4, 1), new DateTime(2023, 4, 30));
                decimal totalWeekUp = DateTimeIncentiveProration.WeekUp(new DateTime(2023, 4, 1), new DateTime(2023, 4, 30));
                decimal totalDays360 = DateTimeIncentiveProration.Days360(new DateTime(2023, 4, 1), new DateTime(2023, 4, 30));

                decimal DaysProration = Math.Round(DateTimeIncentiveProration.Days(data.BonusFromDate, data.BonusToDate) / totalDays * 100, 7);
                decimal MonthRoundProration = Math.Round(DateTimeIncentiveProration.MonthRound(data.BonusFromDate, data.BonusToDate) / totalMonthRound * 100, 7);
                decimal MonthUpProration = Math.Round(DateTimeIncentiveProration.MonthUp(data.BonusFromDate, data.BonusToDate) / totalMonthUp * 100, 7);
                decimal MonthDownProration = Math.Round(DateTimeIncentiveProration.MonthDown(data.BonusFromDate, data.BonusToDate) / totalMonthDown * 100, 7);
                decimal WeekDownProration = Math.Round(DateTimeIncentiveProration.WeekDown(data.BonusFromDate, data.BonusToDate) / totalWeekDown * 100, 7);
                decimal WeekUpProration = Math.Round(DateTimeIncentiveProration.WeekUp(data.BonusFromDate, data.BonusToDate) / totalWeekUp * 100, 7);
                decimal Days360Proration = Math.Round(DateTimeIncentiveProration.Days360(data.BonusFromDate, data.BonusToDate) / totalDays360 * 100, 7);

                Console.WriteLine($"Days Proration: {DaysProration}");
                Console.WriteLine($"WeekDown Proration: {WeekDownProration}");
                Console.WriteLine($"WeekUp Proration: {WeekUpProration}");
                Console.WriteLine($"MonthRound Proration: {MonthRoundProration}");
                Console.WriteLine($"MonthUp Proration: {MonthUpProration}");
                Console.WriteLine($"MonthDown Proration: {MonthDownProration}");
                Console.WriteLine($"Days360 Proration: {Days360Proration}");

                Console.WriteLine("=".PadRight(140, '='));
            }
        }

    }
}
