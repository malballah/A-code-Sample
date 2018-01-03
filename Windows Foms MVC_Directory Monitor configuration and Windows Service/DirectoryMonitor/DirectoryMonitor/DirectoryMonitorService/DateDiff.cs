using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryMonitorService
{
    public class DateDiff
    {
        public int Years;
        public int Months;
        public int Days;
        public int Hours;
        public int Minutes;
        public int Seconds;

        public DateDiff Count(DateTime date1, DateTime date2)
        {
            int DaysInBdayMonth = DateTime.DaysInMonth(date1.Year, date1.Month);
            int DaysRemain = date2.Day + (DaysInBdayMonth - date1.Day);

            if (date2.Month > date1.Month)
            {
                this.Years = date2.Year - date1.Year;
                this.Months = date2.Month - (date1.Month + 1) + Math.Abs(DaysRemain / DaysInBdayMonth);
                this.Days = (DaysRemain % DaysInBdayMonth + DaysInBdayMonth) % DaysInBdayMonth;
            }
            else if (date2.Month == date1.Month)
            {
                if (date2.Day >= date1.Day)
                {
                    this.Years = date2.Year - date1.Year;
                    this.Months = 0;
                    this.Days = date2.Day - date1.Day;
                }
                else
                {
                    this.Years = (date2.Year - 1) - date1.Year;
                    this.Months = 11;
                    this.Days = DateTime.DaysInMonth(date1.Year, date1.Month) - (date1.Day - date2.Day);
                }
            }
            else
            {
                this.Years = (date2.Year - 1) - date1.Year;
                this.Months = date2.Month + (11 - date1.Month) + Math.Abs(DaysRemain / DaysInBdayMonth);
                this.Days = (DaysRemain % DaysInBdayMonth + DaysInBdayMonth) % DaysInBdayMonth;
            }
            var timeSpan = (date2 - date1);
            this.Hours = timeSpan.Hours;
            this.Minutes = timeSpan.Minutes;
            this.Seconds = timeSpan.Seconds;
            return this;
        }
    }
}
