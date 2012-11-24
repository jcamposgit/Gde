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

using System.Collections.Generic;
using ClubStarterKit.Web.Infrastructure.Forum;

namespace ClubStarterKit.Web.ViewData.Forum
{
    public class EditTopicViewData
    {
        public EditTopicViewData()
            : this(null)
        { }

        public EditTopicViewData(TopicViewItem topic)
        {
            Topic = topic ?? new TopicViewItem
            {
                VisibleToAnonymous = true
            };
            Groups = new TopicGroupRetrievalAction().Execute();
        }

        public TopicViewItem Topic { get; set; }
        public IEnumerable<string> Groups { get; set; }
    }
}