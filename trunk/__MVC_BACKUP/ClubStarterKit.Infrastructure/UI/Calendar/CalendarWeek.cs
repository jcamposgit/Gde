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
    public class CalendarWeek : IEventImporter
    {
        public CalendarWeek(DateTimeOffset day, int requestedMonth)
            : this(day.Year, day.Month, day.Day, requestedMonth)
        {
        }

        public CalendarWeek(int year, int month, int day, int requestedMonth)
        {
            DateTime sunday = new DateTime(year, month, day);
            if (sunday.DayOfWeek != DayOfWeek.Sunday)
                sunday = sunday.ToFirstDayOfWeek();

            Sunday = new CalendarDay(sunday, requestedMonth);
            Monday = new CalendarDay(sunday.AddDays(1), requestedMonth);
            Tuesday = new CalendarDay(sunday.AddDays(2), requestedMonth);
            Wednesday = new CalendarDay(sunday.AddDays(3), requestedMonth);
            Thursday = new CalendarDay(sunday.AddDays(4), requestedMonth);
            Friday = new CalendarDay(sunday.AddDays(5), requestedMonth);
            Saturday = new CalendarDay(sunday.AddDays(6), requestedMonth);

            var nextWeek = sunday.AddDays(7);
            var prevWeek = sunday.AddDays(-7);
            NextLink = FormatLink(nextWeek.Year, nextWeek.Month, nextWeek.Day);
            PrevLink = FormatLink(prevWeek.Year, prevWeek.Month, prevWeek.Day);

            Month = sunday.Month;
            Year = sunday.Year;
            Day = sunday.Day;

            CurrentLink = FormatLink(sunday.Year, sunday.Month, sunday.Day);
            MonthLink = CalendarMonth.FormatLink(year, month);
        }

        internal static string FormatLink(int year, int month, int day)
        {
            return "week-" + year + "-" + FormatInt(month) + "-" + FormatInt(day);
        }

        private static string FormatInt(int i)
        {
            // hack to ensure the format contains correct
            // number of characters
            if (i < 10)
                return "0" + i;
            return i.ToString();
        }

        public CalendarDay Sunday { get; private set; }
        public CalendarDay Monday { get; private set; }
        public CalendarDay Tuesday { get; private set; }
        public CalendarDay Wednesday { get; private set; }
        public CalendarDay Thursday { get; private set; }
        public CalendarDay Friday { get; private set; }
        public CalendarDay Saturday { get; private set; }

        public void Import(IEnumerable<ICalendarEvent> events)
        {
            Sunday.Import(events);
            Monday.Import(events);
            Tuesday.Import(events);
            Wednesday.Import(events);
            Thursday.Import(events);
            Friday.Import(events);
            Saturday.Import(events);
        }

        public int Month { get; private set; }
        public string MonthLink { get; private set; }
        public int Year { get; private set; }
        public int Day { get; private set; }
        public string NextLink { get; private set; }
        public string PrevLink { get; private set; }
        public string CurrentLink { get; private set; }
    }
}