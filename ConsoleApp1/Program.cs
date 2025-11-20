//Round proration
using ConsoleApp1;

//ProgramSalary.CalculateAllProration();
ProgramIncentive.CalculateAllProration();
//var salaryData = new List<(DateTime LastSalaryEffDate, DateTime NewSalaryEffDate)>();

//Console.WriteLine("=== Proration Calculator ===");
//Console.WriteLine("Enter date pairs in format: MM/DD/YYYY, MM/DD/YYYY");
//Console.WriteLine("(One pair per line, press Enter on empty line when done)\n");

//while (true)
//{
//    Console.Write("Enter date pair (LastDate, NewDate) or press Enter to finish: ");
//    string input = Console.ReadLine();

//    if (string.IsNullOrWhiteSpace(input))
//        break;

//    var parts = input.Split(',');
//    if (parts.Length != 2)
//    {
//        Console.WriteLine("Invalid format. Use: MM/DD/YYYY, MM/DD/YYYY\n");
//        continue;
//    }

//    if (!DateTime.TryParse(parts[0].Trim(), out DateTime lastDate))
//    {
//        Console.WriteLine("Invalid Last Salary Date format.\n");
//        continue;
//    }

//    if (!DateTime.TryParse(parts[1].Trim(), out DateTime newDate))
//    {
//        Console.WriteLine("Invalid New Salary Date format.\n");
//        continue;
//    }

//    salaryData.Add((lastDate, newDate));
//    Console.WriteLine($"Added: {lastDate:M/d/yyyy} -> {newDate:M/d/yyyy}\n");
//}

//if (salaryData.Count == 0)
//{
//    Console.WriteLine("No data entered. Exiting.");
//    return;
//}

//Console.WriteLine("\n" + "=".PadRight(120, '='));
//Console.WriteLine("Last Salary\tNew Salary\t\tDays\tWeek Down\tWeek Up\tMonth Rnd\tMonth Up\tMonth Down");
//Console.WriteLine("=".PadRight(120, '='));

//foreach (var data in salaryData)
//{
//    int days = DateTimeProration.Days(data.NewSalaryEffDate, data.LastSalaryEffDate);
//    int weekDown = DateTimeProration.WeekDown(data.LastSalaryEffDate, data.NewSalaryEffDate);
//    int weekUp = DateTimeProration.WeekUp(data.LastSalaryEffDate, data.NewSalaryEffDate);
//    int monthRound = DateTimeProration.MonthRound(data.LastSalaryEffDate, data.NewSalaryEffDate);
//    int monthUp = DateTimeProration.MonthUp(data.LastSalaryEffDate, data.NewSalaryEffDate);
//    int monthDown = DateTimeProration.MonthDown(data.LastSalaryEffDate, data.NewSalaryEffDate);

//    Console.WriteLine($"{data.LastSalaryEffDate:M/d/yyyy}\t{data.NewSalaryEffDate:M/d/yyyy}\t\t{days}\t{weekDown}\t\t{weekUp}\t{monthRound}\t\t{monthUp}\t\t{monthDown}");
//}