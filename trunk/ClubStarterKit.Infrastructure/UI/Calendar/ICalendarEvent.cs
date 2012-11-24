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

namespace ClubStarterKit.Infrastructure.UI.Calendar
{
    /// <summary>
    /// Abstraction used for the calendar UI controls 
    /// </summary>
    /// <remarks>Removes dependency on the Domain project</remarks>
    public interface ICalendarEvent
    {
        string Title { get; }

        DateTimeOffset StartDate { get; }

        DateTimeOffset EndDate { get; }

        string Id { get; }

        string Description { get; }
    }

    public static class CalendarEventExt
    {
        public static bool IsContained(this ICalendarEvent calEvent, DateTimeOffset date)
        {
            return date.Date.CompareTo(calEvent.StartDate.Date) >= 0 &&
                   date.Date.CompareTo(calEvent.EndDate.Date) <= 0;
        }
    }
}