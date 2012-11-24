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
using ClubStarterKit.Domain;

namespace ClubStarterKit.Web.ViewData.Forum
{
    public class MessageViewItem
    {
        public string Body { get; set; }
        public DateTimeOffset DateAdded { get; set; }
        public User Member { get; set; }
        public int Id { get; set; }
    }

    public class MessageListViewData
    {
        public IEnumerable<MessageViewItem> Messages { get; set; }
        public bool Locked { get; set; } 
        public string Title { get; set; }
        public string ThreadSlug { get; set; }
    }
}