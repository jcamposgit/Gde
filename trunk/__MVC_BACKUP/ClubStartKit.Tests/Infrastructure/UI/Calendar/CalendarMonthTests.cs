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

using ClubStarterKit.Infrastructure.UI.Calendar;
using Xunit;

namespace ClubStarterKit.Tests.Infrastructure.UI.Calendar
{
    public class CalendarMonthTests
    {
        [Fact]
        public void CalendarMonth_FirstWeek_WhenMonthStartsOnSunday_HasSundayValueEqualToFirstDay()
        {
            var month = new CalendarMonth(2009, 11);

            Assert.Equal(11, month.Weeks[0].Sunday.Date.Month);
            Assert.Equal(1, month.Weeks[0].Sunday.Date.Day);
        }

        [Fact]
        public void CalendarMonth_Weeks_HasFiveWeeks_ForOct2009()
        {
            var month = new CalendarMonth(2009, 10);

            Assert.Equal(5, month.Weeks.Count);
        }

        [Fact]
        public void CalendarMonth_Weeks_HasFiveWeeks_ForNov2009()
        {
            var month = new CalendarMonth(2009, 11);

            Assert.Equal(5, month.Weeks.Count);
        }

        [Fact]
        public void CalendarMonth_Weeks_HasFourWeeks_ForFeb2009()
        {
            var month = new CalendarMonth(2009, 2);
            
            Assert.Equal(4, month.Weeks.Count);
        }
    }
}
