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
        public static int Days(DateTime endDate, DateTime startDate)
        {
            TimeSpan difference = endDate.Date - startDate.Date;
            return (int)difference.TotalDays;
        }

        /// <summary>
        /// Calculates the number of days between two dates (endDate - startDate).
        /// Overload for DateOnly type (available in .NET 6+)
        /// </summary>
        /// <param name="endDate">The end date</param>
        /// <param name="startDate">The start date</param>
        /// <returns>The number of days between the dates</returns>
        public static int Days(DateOnly endDate, DateOnly startDate)
        {
            return endDate.DayNumber - startDate.DayNumber;
        }

        /// <summary>
        /// Calculates rounded months between two dates based on Excel formula logic.
        /// Formula: =IFERROR(IF(AND(MONTH(H25)=MONTH(I25),YEAR(H25)=YEAR(I25),I25-H25>0),IF(I25-H25<DAY(EOMONTH(H25,0))/2,0,1),
        /// DATEDIF(EOMONTH(H25,0),EOMONTH(I25,-1)+1,"M") + (IF(DAY(H25)<=DAY(EOMONTH(H25,0))/2,1,0)) + (IF(DAY(I25)>DAY(EOMONTH(I25,0))/2,1,0))),0)
        /// </summary>
        /// <param name="startDate">The start date (H25 in formula)</param>
        /// <param name="endDate">The end date (I25 in formula)</param>
        /// <returns>The rounded number of months</returns>
        public static int MonthRound(DateTime startDate, DateTime endDate)
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
        public static int MonthUp(DateTime startDate, DateTime endDate)
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
        /// Formula: =IFERROR(IF(AND(MONTH(H25)=MONTH(I25),YEAR(H25)=YEAR(I25),I25-H25>0),IF(I25-H25>=DAY(EOMONTH(H25,0)),1,0),
        /// DATEDIF(EOMONTH(H25,0),EOMONTH(I25,-1)+1,"M") + (IF(DAY(H25)<=DAY(EOMONTH(H25,0))/2,1,0)) + (IF(DAY(I25)>DAY(EOMONTH(I25,0))/2,1,0))),0)
        /// </summary>
        /// <param name="startDate">The start date (H25 in formula)</param>
        /// <param name="endDate">The end date (I25 in formula)</param>
        /// <returns>The calculated months down value</returns>
        public static int MonthDown(DateTime startDate, DateTime endDate)
        {
            try
            {
                // Check if both dates are in the same month and year with positive difference
                if (startDate.Month == endDate.Month && startDate.Year == endDate.Year && endDate > startDate)
                {
                    int daysDifference = (endDate - startDate).Days;
                    int daysInMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
                    
                    // IF(I25-H25>=DAY(EOMONTH(H25,0)),1,0)
                    return daysDifference >= daysInMonth ? 1 : 0;
                }
                else if (endDate <= startDate)
                {
                    return 0;
                }
                else
                {
                    // Different months/years logic (same as MonthRound)
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
        /// Calculates weeks down between two dates based on Excel formula logic.
        /// Formula: =ROUNDDOWN(DAYS(I25,H25)/7,0)
        /// </summary>
        /// <param name="startDate">The start date (H25 in formula)</param>
        /// <param name="endDate">The end date (I25 in formula)</param>
        /// <returns>The number of complete weeks between the dates</returns>
        public static int WeekDown(DateTime startDate, DateTime endDate)
        {
            try
            {
                // DAYS(I25,H25) - Calculate days between dates
                int days = Days(endDate, startDate);
                
                // ROUNDDOWN(days/7,0) - Divide by 7 and round down
                int weeks = (int)Math.Floor(days / 7.0);
                
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
        public static int WeekUp(DateTime startDate, DateTime endDate)
        {
            try
            {
                // DAYS(I24,H24) - Calculate days between dates
                int days = Days(endDate, startDate);
                
                // ROUNDUP(days/7,0) - Divide by 7 and round up
                int weeks = (int)Math.Ceiling(days / 7.0);
                
                return weeks;
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
