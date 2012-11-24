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

using System.Collections.Generic;
using System.Linq;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure.Cache;
using ClubStarterKit.Infrastructure.UI.Calendar;
using StructureMap;
using IBootstrapper = ClubStarterKit.Core.IBootstrapper;

namespace ClubStarterKit.Web.Infrastructure.Calendar
{
    public class CalendarEventService : ICalendarEventService
    {
        #region ICalendarEventService Members

        public IEnumerable<ICalendarEvent> GetAllEvents()
        {
            return new CollectionDataCache<Event>()
                    .With(repo => repo.With(x => x.Downloads).With(x => x.EventLocation))
                    .CachedValue
                    .Select(e => new CalendarEventWrapper(e));
        }

        #endregion
    }

    public class CalendarEventServiceBootstrapper : IBootstrapper
    {
        public void Bootstrap()
        {
            ObjectFactory.Configure(config => config.ForRequestedType<ICalendarEventService>()
                                                    .TheDefaultIsConcreteType<CalendarEventService>());
        }
    }
}