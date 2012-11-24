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
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure.UI.Calendar;

namespace ClubStarterKit.Web.Infrastructure.Calendar
{
    public class CalendarEventWrapper : ICalendarEvent
    {
        private readonly Event _event;

        public CalendarEventWrapper(Event @event)
        {
            _event = @event;
        }

        public string Title
        {
            get { return _event.Title; }
        }

        public DateTimeOffset StartDate
        {
            get { return _event.StartTime; }
        }

        public DateTimeOffset EndDate
        {
            get { return _event.EndTime; }
        }

        public string Id
        {
            get { return _event.Slug; }
        }

        public string Description
        {
            get { return _event.Description; }
        }
    }
}