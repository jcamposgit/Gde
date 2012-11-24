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

using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure.Cache;

namespace ClubStarterKit.Web.Infrastructure.Calendar
{
    public class EventRetrievalAction : IDataAction<string, Event>
    {
        public EventRetrievalAction(string slug)
        {
            Context = slug;
        }

        public string Context { get; private set; }

        public Event Execute()
        {
            return new SingleItemDataCache<Event>(e => e.Slug == Context)
                        .With(repo => repo.With(x => x.EventLocation)
                                          .With(x => x.Downloads))
                        .CachedValue;
        }
    }
}