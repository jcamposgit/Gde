﻿#region license

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

using ClubStarterKit.Core;
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure.Cache;

namespace ClubStarterKit.Web.Infrastructure.Poll
{
    public class PollListAction : IDataAction<int, IPagedList<PollQuestion>>
    {
        public PollListAction(int page)
        {
            Context = page;
        }

        public int Context { get; private set; }

        public IPagedList<PollQuestion> Execute()
        {
            return new PagedDataCache<PollQuestion>().Only(x => !x.Hidden)
                                                     .OnPage(Context)
                                                     .WithPageSize(1)
                                                     .Sorted(p => p.CreationDate, false)
                                                     .CachedValue;

        }
    }
}