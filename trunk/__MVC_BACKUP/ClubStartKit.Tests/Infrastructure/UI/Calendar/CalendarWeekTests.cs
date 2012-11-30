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
using ClubStarterKit.Infrastructure.UI.Calendar;
using Xunit;

namespace ClubStarterKit.Tests.Infrastructure.UI.Calendar
{
    public class CalendarWeekTests
    {
        [Fact]
        public void CalendarWeek_Constructor_GivenSundayDate_SetsSundayProperty()
        {
            var given_sunday = new DateTime(year: 2009, month: 10, day: 4); // oct 4, 2009
            var calendar_week = new CalendarWeek(given_sunday, 10);

            Assert.Equal(given_sunday, calendar_week.Sunday.Date);
        }

        [Fact]
        public void CalendarWeek_Constructor_GivenMondayDate_SundayDateIsCalculated()
        {
            var given_sunday = new DateTime(year: 2009, month: 10, day: 4); // oct 4, 2009
            var given_monday = new DateTime(year: 2009, month: 10, day: 5); // oct 5, 2009
            var calendar_week = new CalendarWeek(given_monday, 10);

            Assert.Equal(given_sunday, calendar_week.Sunday.Date);
        }

        [Fact]
        public void CalendarWeek_Constructor_GivenTuesdayDate_SundayDateIsCalculated()
        {
            var given_sunday = new DateTime(year: 2009, month: 10, day: 4); // oct 4, 2009
            var given_tuesday = new DateTime(year: 2009, month: 10, day: 6); // oct 6, 2009
            var calendar_week = new CalendarWeek(given_tuesday, 10);

            Assert.Equal(given_sunday, calendar_week.Sunday.Date);
        }

        [Fact]
        public void CalendarWeek_Constructor_GivenWednesdayDate_SundayDateIsCalculated()
        {
            var given_sunday = new DateTime(year: 2009, month: 10, day: 4); // oct 4, 2009
            var given_wednesday = new DateTime(year: 2009, month: 10, day: 7); // oct 7, 2009
            var calendar_week = new CalendarWeek(given_wednesday, 10);

            Assert.Equal(given_sunday, calendar_week.Sunday.Date);
        }

        [Fact]
        public void CalendarWeek_Constructor_GivenThursdayDate_SundayDateIsCalculated()
        {
            var given_sunday = new DateTime(year: 2009, month: 10, day: 4); // oct 4, 2009
            var given_thursday = new DateTime(year: 2009, month: 10, day: 8); // oct 8, 2009
            var calendar_week = new CalendarWeek(given_thursday, 10);

            Assert.Equal(given_sunday, calendar_week.Sunday.Date);
        }

        [Fact]
        public void CalendarWeek_Constructor_GivenFridayDate_SundayDateIsCalculated()
        {
            var given_sunday = new DateTime(year: 2009, month: 10, day: 4); // oct 4, 2009
            var given_friday = new DateTime(year: 2009, month: 10, day: 9); // oct 9, 2009
            var calendar_week = new CalendarWeek(given_friday, 10);

            Assert.Equal(given_sunday, calendar_week.Sunday.Date);
        }

        [Fact]
        public void CalendarWeek_Constructor_GivenSaturdayDate_SundayDateIsCalculated()
        {
            var given_sunday = new DateTime(year: 2009, month: 10, day: 4); // oct 4, 2009
            var given_saturday = new DateTime(year: 2009, month: 10, day: 10); // oct 10, 2009
            var calendar_week = new CalendarWeek(given_saturday, 10);

            Assert.Equal(given_sunday, calendar_week.Sunday.Date);
        }
    }
}