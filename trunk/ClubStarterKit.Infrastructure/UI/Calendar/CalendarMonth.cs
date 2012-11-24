#region license

//Copyright 2009 Zack Owens

//Licensed under the Microsoft Public License (Ms-PL) (the "License"); 
//you may not use this file except in compliance with the License. 
//You may obtain a copy of the License at 

//http://clubstarterkit.codeplex.com/license

//Unless required by applicable law or agreed to in writing, software 
//distributed under the License is distributed on an "AS IS" BASIS, 
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//See the License for the specific language governing permissions and 
//limitations under the License. 

#endregion

using System;
using System.Collections.Generic;

namespace ClubStarterKit.Infrastructure.UI.Calendar
{
    public class CalendarMonth : IEventImporter
    {
        public CalendarMonth(int year, int month)
        {
            Weeks = DetermineWeeks(year, month);

            var fNextMonth = new DateTime(year, month, 1).AddMonths(1);
            NextLink = FormatLink(fNextMonth.Year, fNextMonth.Month);

            var fPrevMonth = new DateTime(year, month, 1).AddMonths(-1);
            PrevLink = FormatLink(fPrevMonth.Year, fPrevMonth.Month);
            Year = year;
            Month = month;

            CurrentLink = FormatLink(year, month);
        }

        internal static string FormatLink(int year, int month)
        { 
            return "month-" + year + "-" + FormatInt(month);
        }

        private static string FormatInt(int i)
        {
            // hack to ensure the format contains correct
            // number of characters
            if (i < 10)
                return "0" + i;
            return i.ToString();
        }

        public IList<CalendarWeek> Weeks { get; private set; }

        private static IList<CalendarWeek> DetermineWeeks(int year, int month)
        {
            var init = new DateTimeOffset(new DateTime(year, month, 1)).ToFirstDayOfWeek();
            int daysInMonth = DateTime.DaysInMonth(year, month);
            DateTimeOffset start = init;
            var weeks = new List<CalendarWeek>();

            while (start.Subtract(init).Days < daysInMonth)
            {
                weeks.Add(new CalendarWeek(start, month));
                start = start.AddDays(7);
            }

            return weeks;
        }

        public void Import(IEnumerable<ICalendarEvent> events)
        {
            Weeks.Foreach(week => week.Import(events));
        }

        public int Month { get; private set; }
        public string MonthName
        {
            get
            {
                return new DateTime(Year, Month, 1).ToString("MMMM");
            }
        }

        public int Year { get; private set; }
        public string NextLink { get; private set; }
        public string PrevLink { get; private set; }
        public string WeekLink
        {
            get
            {
                return Weeks[0].CurrentLink;
            }
        }

        public string CurrentLink { get; private set; }
    }
}
