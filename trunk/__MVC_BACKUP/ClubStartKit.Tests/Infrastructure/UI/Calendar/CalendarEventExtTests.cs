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
using Moq;
using Xunit;

namespace ClubStarterKit.Tests.Infrastructure.UI.Calendar
{
    public class CalendarEventExtTests
    {
        [Fact]
        public void CaledarEventExt_Contains_ReturnsTrue_IfStartAndEndDateSame_AndGivenDateIsSameDate()
        {
            int year = 2009, month = 1, day = 3;
            int starthour = 3, endhour = 5;
            var calevent = new Mock<ICalendarEvent>();
            calevent.SetupGet(c => c.StartDate).Returns(new DateTimeOffset(year, month, day, starthour, 0, 0, 0, TimeSpan.Zero));
            calevent.SetupGet(c => c.EndDate).Returns(new DateTimeOffset(year, month, day, endhour, 0, 0, 0, TimeSpan.Zero));

            Assert.True(calevent.Object.IsContained(new DateTimeOffset(year, month, day, starthour, 1, 0, 0, TimeSpan.Zero)));
        }

        [Fact]
        public void CaledarEventExt_Contains_ReturnsFalse_IfStartAndEndDateSame_AndGivenDateIsNotSameDate()
        {
            int year = 2009, month = 1, day = 3;
            int starthour = 3, endhour = 5;
            var calevent = new Mock<ICalendarEvent>();
            calevent.SetupGet(c => c.StartDate).Returns(new DateTimeOffset(year, month, day, starthour, 0, 0, 0, TimeSpan.Zero));
            calevent.SetupGet(c => c.EndDate).Returns(new DateTimeOffset(year, month, day, endhour, 0, 0, 0, TimeSpan.Zero));

            Assert.False(calevent.Object.IsContained(new DateTimeOffset(year, month, day + 1, starthour, 1, 0, 0, TimeSpan.Zero)));
        }

        [Fact]
        public void CaledarEventExt_Contains_ReturnsFalse_IfStartAndEndDateDifferent_AndGivenDateIsNotInside()
        {
            int year = 2009, month = 1, day = 3;
            int starthour = 3, endhour = 5;
            var calevent = new Mock<ICalendarEvent>();
            calevent.SetupGet(c => c.StartDate).Returns(new DateTimeOffset(year, month, day, starthour, 0, 0, 0, TimeSpan.Zero));
            calevent.SetupGet(c => c.EndDate).Returns(new DateTimeOffset(year, month, day + 1, endhour, 0, 0, 0, TimeSpan.Zero));

            Assert.False(calevent.Object.IsContained(new DateTimeOffset(year, month, day + 4, starthour, 1, 0, 0, TimeSpan.Zero)));
        }

        [Fact]
        public void CaledarEventExt_Contains_ReturnsTrue_IfStartAndEndDateDifferent_AndGivenDateIsInside()
        {
            int year = 2009, month = 1, day = 3;
            int starthour = 3, endhour = 5;
            var calevent = new Mock<ICalendarEvent>();
            calevent.SetupGet(c => c.StartDate).Returns(new DateTimeOffset(year, month, day, starthour, 0, 0, 0, TimeSpan.Zero));
            calevent.SetupGet(c => c.EndDate).Returns(new DateTimeOffset(year, month, day + 4, endhour, 0, 0, 0, TimeSpan.Zero));

            Assert.True(calevent.Object.IsContained(new DateTimeOffset(year, month, day + 1, starthour, 1, 0, 0, TimeSpan.Zero)));
        }
    }
}
