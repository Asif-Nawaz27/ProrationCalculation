namespace ConsoleApp1
{
    public static class DateTimeIncentiveProration
    {
        /// <summary>
        /// Calculates the number of days between two dates with end date adjusted by +1 day.
        /// Formula: =DAYS(I33+1,H33) which means DAYS(endDate+1, startDate)
        /// </summary>
        /// <param name="endDate">The end date (I33 in formula)</param>
        /// <param name="startDate">The start date (H33 in formula)</param>
        /// <returns>The number of days between startDate and (endDate + 1 day)</returns>
        public static int Days(DateTime endDate, DateTime startDate)
        {
            // DAYS(I33+1,H33) - Add 1 day to endDate before calculating
            DateTime endDatePlusOne = endDate.AddDays(1);
            TimeSpan difference = endDatePlusOne.Date - startDate.Date;
            return (int)difference.TotalDays;
        }

        /// <summary>
        /// Calculates rounded months between two dates for incentive proration.
        /// Formula: =IFERROR(IF(AND(MONTH(H50)=MONTH(I50),YEAR(H50)=YEAR(I50),I50-H50>0),IF(I50-H50+1<DAY(EOMONTH(H50,0))/2,0,1),
        /// DATEDIF(EOMONTH(H50,0),EOMONTH(I50,-1)+1,"M") + (IF(DAY(H50)<=DAY(EOMONTH(H50,0))/2,1,0)) + (IF(DAY(I50)>DAY(EOMONTH(I50,0))/2,1,0))),0)
        /// </summary>
        /// <param name="startDate">The start date (H50 in formula)</param>
        /// <param name="endDate">The end date (I50 in formula)</param>
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
                    
                    // IF(I50-H50+1<DAY(EOMONTH(H50,0))/2,0,1)
                    // Note: +1 is added to the days difference
                    return (daysDifference + 1) < halfMonth ? 0 : 1;
                }
                else if (endDate <= startDate)
                {
                    return 0;
                }
                else
                {
                    // Different months/years logic
                    // EOMONTH(H50,0) - End of month for startDate
                    DateTime endOfStartMonth = new DateTime(startDate.Year, startDate.Month, 
                        DateTime.DaysInMonth(startDate.Year, startDate.Month));
                    
                    // EOMONTH(I50,-1) - End of previous month before endDate
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
                    
                    // EOMONTH(I50,-1)+1 - First day of endDate's month
                    DateTime firstDayOfEndMonth = endOfPreviousMonth.AddDays(1);
                    
                    // DATEDIF(EOMONTH(H50,0),EOMONTH(I50,-1)+1,"M") - Full months between
                    int fullMonths = 0;
                    if (firstDayOfEndMonth > endOfStartMonth)
                    {
                        fullMonths = ((firstDayOfEndMonth.Year - endOfStartMonth.Year) * 12) + 
                                    (firstDayOfEndMonth.Month - endOfStartMonth.Month) - 1;
                        fullMonths = Math.Max(0, fullMonths);
                    }
                    
                    // (IF(DAY(H50)<=DAY(EOMONTH(H50,0))/2,1,0))
                    int daysInStartMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
                    int startDayBonus = startDate.Day <= (daysInStartMonth / 2.0) ? 1 : 0;
                    
                    // (IF(DAY(I50)>DAY(EOMONTH(I50,0))/2,1,0))
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
        /// Calculates months up between two dates for incentive proration.
        /// Formula: =(YEAR(I50)-YEAR(H50))*12+MONTH(I50)+1-MONTH(H50)
        /// </summary>
        /// <param name="startDate">The start date (H50 in formula)</param>
        /// <param name="endDate">The end date (I50 in formula)</param>
        /// <returns>The calculated months up value</returns>
        public static int MonthUp(DateTime startDate, DateTime endDate)
        {
            try
            {
                // (YEAR(I50)-YEAR(H50))*12
                int yearsDiff = (endDate.Year - startDate.Year) * 12;
                
                // +MONTH(I50)
                int endMonth = endDate.Month;
                
                // +1-MONTH(H50)
                int result = yearsDiff + endMonth + 1 - startDate.Month;
                
                return result;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Calculates months down between two dates for incentive proration.
        /// Formula: =IFERROR(IF(AND(MONTH(H50)=MONTH(I50),YEAR(H50)=YEAR(I50),I50-H50>0),IF(I50-H50+1>=DAY(EOMONTH(H50,0)),1,0),
        /// DATEDIF(EOMONTH(H50,0),EOMONTH(I50,-1)+1,"M") + (IF(DAY(H50)<=DAY(EOMONTH(H50,0))/2,1,0)) + (IF(DAY(I50)>DAY(EOMONTH(I50,0))/2,1,0))),0)
        /// </summary>
        /// <param name="startDate">The start date (H50 in formula)</param>
        /// <param name="endDate">The end date (I50 in formula)</param>
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
                    
                    // IF(I50-H50+1>=DAY(EOMONTH(H50,0)),1,0)
                    // Note: +1 is added to the days difference
                    return (daysDifference + 1) >= daysInMonth ? 1 : 0;
                }
                else if (endDate <= startDate)
                {
                    return 0;
                }
                else
                {
                    // Different months/years logic (same as MonthRound)
                    // EOMONTH(H50,0) - End of month for startDate
                    DateTime endOfStartMonth = new DateTime(startDate.Year, startDate.Month, 
                        DateTime.DaysInMonth(startDate.Year, startDate.Month));
                    
                    // EOMONTH(I50,-1) - End of previous month before endDate
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
                    
                    // EOMONTH(I50,-1)+1 - First day of endDate's month
                    DateTime firstDayOfEndMonth = endOfPreviousMonth.AddDays(1);
                    
                    // DATEDIF(EOMONTH(H50,0),EOMONTH(I50,-1)+1,"M") - Full months between
                    int fullMonths = 0;
                    if (firstDayOfEndMonth > endOfStartMonth)
                    {
                        fullMonths = ((firstDayOfEndMonth.Year - endOfStartMonth.Year) * 12) + 
                                    (firstDayOfEndMonth.Month - endOfStartMonth.Month) - 1;
                        fullMonths = Math.Max(0, fullMonths);
                    }
                    
                    // (IF(DAY(H50)<=DAY(EOMONTH(H50,0))/2,1,0))
                    int daysInStartMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
                    int startDayBonus = startDate.Day <= (daysInStartMonth / 2.0) ? 1 : 0;
                    
                    // (IF(DAY(I50)>DAY(EOMONTH(I50,0))/2,1,0))
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
        /// Calculates weeks down between two dates for incentive proration.
        /// Formula: =ROUNDDOWN(DAYS(I50+1,H50)/7,0)
        /// </summary>
        /// <param name="startDate">The start date (H50 in formula)</param>
        /// <param name="endDate">The end date (I50 in formula)</param>
        /// <returns>The number of complete weeks between the dates</returns>
        public static int WeekDown(DateTime startDate, DateTime endDate)
        {
            try
            {
                // DAYS(I50+1,H50) - Calculate days with endDate+1
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
        /// Calculates weeks up between two dates for incentive proration.
        /// Formula: =ROUNDUP(DAYS(I50+1,H50)/7,0)
        /// </summary>
        /// <param name="startDate">The start date (H50 in formula)</param>
        /// <param name="endDate">The end date (I50 in formula)</param>
        /// <returns>The number of weeks between the dates, rounded up</returns>
        public static int WeekUp(DateTime startDate, DateTime endDate)
        {
            try
            {
                // DAYS(I50+1,H50) - Calculate days with endDate+1
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
        /// Calculates the number of days between two dates based on a 360-day year (12 30-day months).
        /// Formula: =DAYS360(H50,I50)
        /// Uses the US (NASD) method by default.
        /// </summary>
        /// <param name="startDate">The start date (H50 in formula)</param>
        /// <param name="endDate">The end date (I50 in formula)</param>
        /// <returns>The number of days between the dates using 360-day year calculation</returns>
        public static int Days360(DateTime startDate, DateTime endDate)
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
    }
}
