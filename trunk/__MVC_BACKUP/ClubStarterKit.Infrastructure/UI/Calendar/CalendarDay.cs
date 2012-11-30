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
using System.Linq;

namespace ClubStarterKit.Infrastructure.UI.Calendar
{
    public class CalendarDay : IEventImporter
    {
        public CalendarDay(DateTimeOffset date, int requestedMonth)
        {
            Date = date.Date;
            IsOtherMonth = Date.Month != requestedMonth;
        }

        public bool IsOtherMonth { get; private set; }

        public bool IsWeekend
        {
            get
            {
                return Date.DayOfWeek == DayOfWeek.Sunday ||
                       Date.DayOfWeek == DayOfWeek.Saturday;
            }
        }

        public bool IsToday
        {
            get
            {
                return Date.EqualDate(DateTimeOffset.UtcNow);
            }
        }

        public DateTimeOffset Date { get; private set; }

        public override string ToString()
        {
            return Date.UtcDateTime.ToShortDateString();
        }

        private readonly List<ICalendarEvent> _events = new List<ICalendarEvent>();

        public IEnumerable<ICalendarEvent> Events
        {
            get { return _events; }
        }

        public void Import(IEnumerable<ICalendarEvent> events)
        {
            _events.AddRange(events.Where(e => e.IsContained(Date)));

            // sort the events
            Comparison<ICalendarEvent> comp = (x, y) => x.StartDate.CompareTo(y.StartDate);
            _events.Sort(comp);
        }
    }
}