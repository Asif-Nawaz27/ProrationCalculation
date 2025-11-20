namespace ConsoleApp1
{
    public static class DateTimeSalaryProration
    {
        /// <summary>
        /// Calculates the number of days between two dates (endDate - startDate).
        /// Works like Excel's DAYS function: DAYS(end_date, start_date)
        /// </summary>
        /// <param name="endDate">The end date</param>
        /// <param name="startDate">The start date</param>
        /// <returns>The number of days between the dates (can be negative if endDate is before startDate)</returns>
        public static decimal Days(DateTime startDate,DateTime endDate)
        {
            TimeSpan difference = endDate.Date - startDate.Date;
            return (decimal)difference.TotalDays;
        }

    
        /// <summary>
        /// Calculates rounded months between two dates based on Excel formula logic.
        /// Formula: =IFERROR(IF(AND(MONTH(H25)=MONTH(I25),YEAR(H25)=YEAR(I25),I25-H25>0),IF(I25-H25<DAY(EOMONTH(H25,0))/2,0,1),
        /// DATEDIF(EOMONTH(H25,0),EOMONTH(I25,-1)+1,"M") + (IF(DAY(H25)<=DAY(EOMONTH(H25,0))/2,1,0)) + (IF(DAY(I25)>DAY(EOMONTH(I25,0))/2,1,0))),0)
        /// </summary>
        /// <param name="startDate">The start date (H25 in formula)</param>
        /// <param name="endDate">The end date (I25 in formula)</param>
        /// <returns>The rounded number of months</returns>
        public static decimal MonthRound(DateTime startDate, DateTime endDate)
        {
            try
            {
                // Check if both dates are in the same month and year with positive difference
                if (startDate.Month == endDate.Month && startDate.Year == endDate.Year && endDate > startDate)
                {
                    int daysDifference = (endDate - startDate).Days;
                    int daysInMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
                    double halfMonth = daysInMonth / 2.0;
                    
                    // IF(I25-H25<DAY(EOMONTH(H25,0))/2,0,1)
                    return daysDifference < halfMonth ? 0 : 1;
                }
                else if (endDate <= startDate)
                {
                    return 0;
                }
                else
                {
                    // Different months/years logic
                    // EOMONTH(H25,0) - End of month for startDate
                    DateTime endOfStartMonth = new DateTime(startDate.Year, startDate.Month, 
                        DateTime.DaysInMonth(startDate.Year, startDate.Month));
                    
                    // EOMONTH(I25,-1) - End of previous month before endDate
                    DateTime endOfPreviousMonth;
                    if (endDate.Month == 1)
                    {
                        endOfPreviousMonth = new DateTime(endDate.Year - 1, 12, 31);
                    }
                    else
                    {
                        int prevMonth = endDate.Month - 1;
                        endOfPreviousMonth = new DateTime(endDate.Year, prevMonth, 
                            DateTime.DaysInMonth(endDate.Year, prevMonth));
                    }
                    
                    // EOMONTH(I25,-1)+1 - First day of endDate's month
                    DateTime firstDayOfEndMonth = endOfPreviousMonth.AddDays(1);
                    
                    // DATEDIF(EOMONTH(H25,0),EOMONTH(I25,-1)+1,"M") - Full months between
                    int fullMonths = 0;
                    if (firstDayOfEndMonth > endOfStartMonth)
                    {
                        fullMonths = ((firstDayOfEndMonth.Year - endOfStartMonth.Year) * 12) + 
                                    (firstDayOfEndMonth.Month - endOfStartMonth.Month) - 1;
                        fullMonths = Math.Max(0, fullMonths);
                    }
                    
                    // (IF(DAY(H25)<=DAY(EOMONTH(H25,0))/2,1,0))
                    int daysInStartMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
                    int startDayBonus = startDate.Day <= (daysInStartMonth / 2.0) ? 1 : 0;
                    
                    // (IF(DAY(I25)>DAY(EOMONTH(I25,0))/2,1,0))
                    int daysInEndMonth = DateTime.DaysInMonth(endDate.Year, endDate.Month);
                    int endDayBonus = endDate.Day > (daysInEndMonth / 2.0) ? 1 : 0;
                    
                    return fullMonths + startDayBonus + endDayBonus;
                }
            }
            catch
            {
                return 0; // IFERROR returns 0 on error
            }
        }

        /// <summary>
        /// Calculates months up between two dates based on Excel formula logic.
        /// Formula: =(YEAR(I25-1)-YEAR(H25))*12+MONTH(I25-1)+1-MONTH(H25)
        /// </summary>
        /// <param name="startDate">The start date (H25 in formula)</param>
        /// <param name="endDate">The end date (I25 in formula)</param>
        /// <returns>The calculated months up value</returns>
        public static decimal MonthUp(DateTime startDate, DateTime endDate)
        {
            try
            {
                // I25-1 means subtract 1 day from endDate
                DateTime endDateMinusOne = endDate.AddDays(-1);
                
                // (YEAR(I25-1)-YEAR(H25))*12
                int yearsDiff = (endDateMinusOne.Year - startDate.Year) * 12;
                
                // +MONTH(I25-1)
                int endMonth = endDateMinusOne.Month;
                
                // +1-MONTH(H25)
                int result = yearsDiff + endMonth + 1 - startDate.Month;
                
                return result;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Calculates months down between two dates based on Excel formula logic.
        /// Formula: =IFERROR(IF(AND(MONTH(H8)=MONTH(I8),YEAR(H8)=YEAR(I8),I8-H8>0),IF(I8-H8+1>=DAY(EOMONTH(H8,0)),1,0),
        /// DATEDIF(EOMONTH(H8,0),EOMONTH(I8,-1)+1,"M")+(IF(DAY(H8)=1,1,0))+(IF(DAY(I8)=DAY(EOMONTH(I8,0)),1,0))),0)
        /// </summary>
        /// <param name="startDate">The start date (H8 in formula)</param>
        /// <param name="endDate">The end date (I8 in formula)</param>
        /// <returns>The calculated months down value</returns>
        public static decimal MonthDown(DateTime startDate, DateTime endDate)
        {
            try
            {
                // Check if both dates are in the same month and year with positive difference
                if (startDate.Month == endDate.Month && startDate.Year == endDate.Year && endDate > startDate)
                {
                    int daysDifference = (endDate - startDate).Days;
                    int daysInMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
                    
                    // IF(I8-H8+1>=DAY(EOMONTH(H8,0)),1,0)
                    // Note: +1 is added to the days difference
                    return (daysDifference + 1) >= daysInMonth ? 1 : 0;
                }
                else if (endDate <= startDate)
                {
                    return 0;
                }
                else
                {
                    // Different months/years logic
                    // EOMONTH(H8,0) - End of month for startDate
                    DateTime endOfStartMonth = new DateTime(startDate.Year, startDate.Month, 
                        DateTime.DaysInMonth(startDate.Year, startDate.Month));
                    
                    // EOMONTH(I8,-1) - End of previous month before endDate
                    DateTime endOfPreviousMonth;
                    if (endDate.Month == 1)
                    {
                        endOfPreviousMonth = new DateTime(endDate.Year - 1, 12, 31);
                    }
                    else
                    {
                        int prevMonth = endDate.Month - 1;
                        endOfPreviousMonth = new DateTime(endDate.Year, prevMonth, 
                            DateTime.DaysInMonth(endDate.Year, prevMonth));
                    }
                    
                    // EOMONTH(I8,-1)+1 - First day of endDate's month
                    DateTime firstDayOfEndMonth = endOfPreviousMonth.AddDays(1);
                    
                    // DATEDIF(EOMONTH(H8,0),EOMONTH(I8,-1)+1,"M") - Full months between
                    int fullMonths = 0;
                    if (firstDayOfEndMonth > endOfStartMonth)
                    {
                        fullMonths = ((firstDayOfEndMonth.Year - endOfStartMonth.Year) * 12) + 
                                    (firstDayOfEndMonth.Month - endOfStartMonth.Month) - 1;
                        fullMonths = Math.Max(0, fullMonths);
                    }
                    
                    // (IF(DAY(H8)=1,1,0)) - Add 1 if start date is the 1st of the month
                    int startDayBonus = startDate.Day == 1 ? 1 : 0;
                    
                    // (IF(DAY(I8)=DAY(EOMONTH(I8,0)),1,0)) - Add 1 if end date is the last day of the month
                    int daysInEndMonth = DateTime.DaysInMonth(endDate.Year, endDate.Month);
                    int endDayBonus = endDate.Day == daysInEndMonth ? 1 : 0;
                    
                    return fullMonths + startDayBonus + endDayBonus;
                }
            }
            catch
            {
                return 0; // IFERROR returns 0 on error
            }
        }

        /// <summary>
        /// Calculates weeks down between two dates based on Excel formula logic.
        /// Formula: =ROUNDDOWN(DAYS(I25,H25)/7,0)
        /// </summary>
        /// <param name="startDate">The start date (H25 in formula)</param>
        /// <param name="endDate">The end date (I25 in formula)</param>
        /// <returns>The number of complete weeks between the dates</returns>
        public static decimal WeekDown(DateTime startDate, DateTime endDate)
        {
            try
            {
                // DAYS(I25,H25) - Calculate days between dates
                decimal days = Days(startDate,endDate);
                
                // ROUNDDOWN(days/7,0) - Divide by 7 and round down
                decimal weeks = Math.Floor(days / 7.0m);
                
                return weeks;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Calculates weeks up between two dates based on Excel formula logic.
        /// Formula: =ROUNDUP(DAYS(I24,H24)/7,0)
        /// </summary>
        /// <param name="startDate">The start date (H24 in formula)</param>
        /// <param name="endDate">The end date (I24 in formula)</param>
        /// <returns>The number of weeks between the dates, rounded up</returns>
        public static decimal WeekUp(DateTime startDate, DateTime endDate)
        {
            try
            {
                // DAYS(I24,H24) - Calculate days between dates
                decimal days = Days(startDate, endDate);
                
                // ROUNDUP(days/7,0) - Divide by 7 and round up
                decimal weeks = Math.Ceiling(days / 7.0m);
                
                return weeks;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Calculates the number of days between two dates based on a 360-day year (12 30-day months).
        /// Formula: =DAYS360(H50,I50)
        /// Uses the US (NASD) method by default.
        /// </summary>
        /// <param name="startDate">The start date (H50 in formula)</param>
        /// <param name="endDate">The end date (I50 in formula)</param>
        /// <returns>The number of days between the dates using 360-day year calculation</returns>
        public static decimal Days360(DateTime startDate, DateTime endDate)
        {
            try
            {
                int startDay = startDate.Day;
                int startMonth = startDate.Month;
                int startYear = startDate.Year;

                int endDay = endDate.Day;
                int endMonth = endDate.Month;
                int endYear = endDate.Year;

                // US (NASD) Method
                // If the starting date is the 31st, change it to the 30th
                if (startDay == 31)
                {
                    startDay = 30;
                }

                // If the ending date is the 31st and the starting date is earlier than the 30th,
                // change the ending date to the 1st of the next month
                // Otherwise, change it to the 30th
                if (endDay == 31)
                {
                    if (startDay < 30)
                    {
                        endDay = 1;
                        endMonth++;
                        if (endMonth > 12)
                        {
                            endMonth = 1;
                            endYear++;
                        }
                    }
                    else
                    {
                        endDay = 30;
                    }
                }

                // Calculate days using 360-day year formula
                int days360 = ((endYear - startYear) * 360) + ((endMonth - startMonth) * 30) + (endDay - startDay);

                return days360;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Determines if the date range between startDate and endDate contains a leap day (February 29).
        /// Returns true only if February 29 falls within or between the start and end dates.
        /// Example: 3/1/2023 to 3/1/2024 = true (contains Feb 29, 2024)
        /// Example: 2/1/2023 to 2/1/2024 = false (Feb 29, 2024 is after 2/1/2024)
        /// </summary>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <returns>True if the range contains a leap day, otherwise false</returns>
        public static bool IsLeapYear(DateTime startDate, DateTime endDate)
        {
            try
            {
                // Ensure startDate is before endDate
                if (startDate > endDate)
                {
                    // Swap if needed
                    (startDate, endDate) = (endDate, startDate);
                }

                // Check each year in the range for leap day
                for (int year = startDate.Year; year <= endDate.Year; year++)
                {
                    if (DateTime.IsLeapYear(year))
                    {
                        // Create the leap day for this year
                        DateTime leapDay = new DateTime(year, 2, 29);
                        
                        // Check if the leap day falls within the date range
                        if (leapDay >= startDate && leapDay <= endDate)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
