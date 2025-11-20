namespace ConsoleApp1
{
    public class ProgramSalary
    {
        public static void CalculateAllProration()
        {
            // Data from the attached image
            var salaryData = new[]
            {
                new { LastSalaryEffDate = new DateTime(2023, 4, 1), NewSalaryEffDate = new DateTime(2023, 4, 1) },
                new { LastSalaryEffDate = new DateTime(2023, 4, 1), NewSalaryEffDate = new DateTime(2023, 4, 2) },
                new { LastSalaryEffDate = new DateTime(2023, 4, 1), NewSalaryEffDate = new DateTime(2023, 4, 14) },
                new { LastSalaryEffDate = new DateTime(2023, 4, 1), NewSalaryEffDate = new DateTime(2023, 4, 15) },
                new { LastSalaryEffDate = new DateTime(2023, 4, 1), NewSalaryEffDate = new DateTime(2023, 4, 16) },
                new { LastSalaryEffDate = new DateTime(2023, 4, 1), NewSalaryEffDate = new DateTime(2023, 4, 29) },
                new { LastSalaryEffDate = new DateTime(2023, 4, 1), NewSalaryEffDate = new DateTime(2023, 4, 30) },
                new { LastSalaryEffDate = new DateTime(2023, 4, 1), NewSalaryEffDate = new DateTime(2023, 5, 1) },
                new { LastSalaryEffDate = new DateTime(2023, 4, 1), NewSalaryEffDate = new DateTime(2023, 5, 2) },
                new { LastSalaryEffDate = new DateTime(2023, 1, 1), NewSalaryEffDate = new DateTime(2024, 4, 1) },
                new { LastSalaryEffDate = new DateTime(2023, 1, 2), NewSalaryEffDate = new DateTime(2024, 4, 1) },
                new { LastSalaryEffDate = new DateTime(2023, 1, 14), NewSalaryEffDate = new DateTime(2024, 4, 1) },
                new { LastSalaryEffDate = new DateTime(2023, 1, 15), NewSalaryEffDate = new DateTime(2024, 4, 1) },
                new { LastSalaryEffDate = new DateTime(2023, 1, 16), NewSalaryEffDate = new DateTime(2024, 4, 1) },
                new { LastSalaryEffDate = new DateTime(2023, 1, 30), NewSalaryEffDate = new DateTime(2024, 4, 1) },
                new { LastSalaryEffDate = new DateTime(2023, 1, 31), NewSalaryEffDate = new DateTime(2024, 4, 1) },
                new { LastSalaryEffDate = new DateTime(2023, 2, 1), NewSalaryEffDate = new DateTime(2024, 4, 1) },
                new { LastSalaryEffDate = new DateTime(2023, 2, 2), NewSalaryEffDate = new DateTime(2024, 4, 1) },
                
            };
           
            Console.WriteLine("Last Salary Eff Date\tNew Salary Eff Date\tDays\tMonthRound\tMonthUp\tMonthDown\tWeekDown\tWeekUp\tIsLeapYear");
            Console.WriteLine("=".PadRight(140, '='));

            foreach (var data in salaryData)
            {
                decimal days = DateTimeSalaryProration.Days(data.LastSalaryEffDate, data.NewSalaryEffDate);
                decimal MonthRound = DateTimeSalaryProration.MonthRound(data.LastSalaryEffDate, data.NewSalaryEffDate);
                decimal MonthUp = DateTimeSalaryProration.MonthUp(data.LastSalaryEffDate, data.NewSalaryEffDate);
                decimal MonthDown = DateTimeSalaryProration.MonthDown(data.LastSalaryEffDate, data.NewSalaryEffDate);
                decimal WeekDown = DateTimeSalaryProration.WeekDown(data.LastSalaryEffDate, data.NewSalaryEffDate);
                decimal WeekUp = DateTimeSalaryProration.WeekUp(data.LastSalaryEffDate, data.NewSalaryEffDate);
                bool isLeapYear = DateTimeSalaryProration.IsLeapYear(data.LastSalaryEffDate, data.NewSalaryEffDate);
                
                Console.WriteLine($"{data.LastSalaryEffDate:M/d/yyyy}\t\t{data.NewSalaryEffDate:M/d/yyyy}\t\t{days}\t{MonthRound}\t\t{MonthUp}\t{MonthDown}\t\t{WeekDown}\t\t{WeekUp}\t\t{isLeapYear}");

                decimal totalDays = isLeapYear ? 366 : 365;
                decimal DaysProration = Math.Round(DateTimeSalaryProration.Days(data.LastSalaryEffDate, data.NewSalaryEffDate) / totalDays * 100, 7);
                decimal MonthRoundProration = Math.Round(DateTimeSalaryProration.MonthRound(data.LastSalaryEffDate, data.NewSalaryEffDate) / 12 * 100, 7);
                decimal MonthUpProration = Math.Round(DateTimeSalaryProration.MonthUp(data.LastSalaryEffDate, data.NewSalaryEffDate) / 12 * 100, 7);
                decimal MonthDownProration = Math.Round(DateTimeSalaryProration.MonthDown(data.LastSalaryEffDate, data.NewSalaryEffDate) / 12 * 100, 7);
                decimal WeekDownProration = Math.Round(DateTimeSalaryProration.WeekDown(data.LastSalaryEffDate, data.NewSalaryEffDate) / 52 * 100, 7);
                decimal WeekUpProration = Math.Round(DateTimeSalaryProration.WeekUp(data.LastSalaryEffDate, data.NewSalaryEffDate) / 53 * 100, 7);
                decimal Days360Proration = Math.Round(DateTimeSalaryProration.Days360(data.LastSalaryEffDate, data.NewSalaryEffDate) / 360 * 100, 7);

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
