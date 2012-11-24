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

using System;
using System.Collections.Generic;

namespace ClubStarterKit.Web.ViewData.Forum
{
    public class TopicViewItem
    {
        public TopicViewItem()
        {
            Id = 0;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? LastUpdate { get; set; }
        public bool HasUnread { get; set; }
        public int TotalThreads { get; set; }
        public string Group { get; set; }
        public string TopicSlug { get; set; }
        public bool VisibleToAnonymous { get; set; }
        public int Id { get; set; }
    }

    public class TopicGroupItem
    {
        public IEnumerable<TopicViewItem> Topics { get; set; }
        public string Group { get; set; }
    }
}