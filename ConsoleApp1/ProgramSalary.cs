namespace ConsoleApp1
{
    public class ProgramSalary
    {
        public static void CalculateAllDays()
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
                decimal periodDays = DateTimeSalaryProration.IsLeapYear(data.LastSalaryEffDate, data.NewSalaryEffDate) ? 366 : 365;
                decimal periodMonths = 12;
                decimal periodWeeks = 52;

                int days = DateTimeSalaryProration.Days(data.NewSalaryEffDate, data.LastSalaryEffDate);
                int MonthRound = DateTimeSalaryProration.MonthRound(data.LastSalaryEffDate, data.NewSalaryEffDate);
                int MonthUp = DateTimeSalaryProration.MonthUp(data.LastSalaryEffDate, data.NewSalaryEffDate);
                int MonthDown = DateTimeSalaryProration.MonthDown(data.LastSalaryEffDate, data.NewSalaryEffDate);
                int WeekDown = DateTimeSalaryProration.WeekDown(data.LastSalaryEffDate, data.NewSalaryEffDate);
                int WeekUp = DateTimeSalaryProration.WeekUp(data.LastSalaryEffDate, data.NewSalaryEffDate);
                bool isLeapYear = DateTimeSalaryProration.IsLeapYear(data.LastSalaryEffDate, data.NewSalaryEffDate);
                
                
                
                Console.WriteLine($"{data.LastSalaryEffDate:M/d/yyyy}\t\t{data.NewSalaryEffDate:M/d/yyyy}\t\t{days}\t{MonthRound}\t\t{MonthUp}\t{MonthDown}\t\t{WeekDown}\t\t{WeekUp}\t\t{isLeapYear}");
                decimal daysProration = Math.Round(DateTimeSalaryProration.Days(data.NewSalaryEffDate, data.LastSalaryEffDate) / periodDays * 100, 7);
                Console.WriteLine($"Days proration: {daysProration}");

                decimal MonthRoundProration = Math.Round(DateTimeSalaryProration.MonthRound(data.LastSalaryEffDate, data.NewSalaryEffDate) / periodMonths * 100, 7);
                Console.WriteLine($"MonthRound proration: {MonthRoundProration}");

                Console.WriteLine("=".PadRight(140, '='));
            }
        }
    }
}
