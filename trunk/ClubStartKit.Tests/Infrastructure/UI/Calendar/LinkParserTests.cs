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
using ClubStarterKit.Infrastructure.UI.Calendar;
using StructureMap;
using Xunit;

namespace ClubStarterKit.Tests.Infrastructure.UI.Calendar
{
    public class LinkParserTests
    {
        public class CalService : ICalendarEventService
        {
            public IEnumerable<ICalendarEvent> GetAllEvents()
            {
                return new List<ICalendarEvent>();
            }
        }

        public LinkParserTests()
        {
            ObjectFactory.Configure(x => x.ForRequestedType<ICalendarEventService>().TheDefaultIsConcreteType<CalService>());
        }

        [Fact]
        public void LinkParser_Calendar_NullId_ThrowsException()
        {
            Assert.Throws<Exception>(() => new LinkParser(null).Calendar);
        }

        [Fact]
        public void LinkParser_Calendar_EmptyId_ThrowsException()
        {
            Assert.Throws<Exception>(() => new LinkParser(string.Empty).Calendar);
        }

        [Fact]
        public void LinkParser_Calendar_IdStartsWithRandomString_ThrowsException()
        {
            Assert.Throws<Exception>(() => new LinkParser("RANDOM!").Calendar);
        }

        [Fact]
        public void LinkParser_Calendar_IdStartsWithMonth_DoesNotThrowsException()
        {
            Assert.DoesNotThrow(() => { var x = new LinkParser("month-2009-12").Calendar; });
        }

        [Fact]
        public void LinkParser_Calendar_IdStartsWithWeek_DoesNotThrowsException()
        {
            Assert.DoesNotThrow(() => { var x = new LinkParser("week-2009-12-11").Calendar; });
        }

        [Fact]
        public void LinkParser_Calendar_Id_WithIncorrectWeekLength_ThrowsException()
        {
            Assert.Throws<Exception>(() => new LinkParser("week-11").Calendar);
        }

        [Fact]
        public void LinkParser_Calendar_Id_WithIncorrectMonthLength_ThrowsException()
        {
            Assert.Throws<Exception>(() => new LinkParser("month-2009-1").Calendar);
        }

        [Fact]
        public void LinkParser_Calendar_IdStartsWithWeek_ReturnsCalendarWeek()
        {
            Assert.IsType<CalendarWeek>(new LinkParser("week-2009-12-11").Calendar);
        }

        [Fact]
        public void LinkParser_Calendar_IdStartsWithMonth_ReturnsCalendarMonth()
        {
            Assert.IsType<CalendarMonth>(new LinkParser("month-2009-12").Calendar);
        }

        [Fact]
        public void LinkParser_Calendar_Id_MonthWithNonNumericChar_ThrowsException()
        {
            Assert.Throws<Exception>(() => new LinkParser("month-20f0-22").Calendar);
        }

        [Fact]
        public void LinkParser_Calendar_Id_WeekWithNonNumericChar_ThrowsException()
        {
            Assert.Throws<Exception>(() => new LinkParser("week-2009-11-u3").Calendar);
        }
    }
}
