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
using System.Linq;
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure.Cache;
using ClubStarterKit.Web.ViewData.Forum;

namespace ClubStarterKit.Web.Infrastructure.Forum
{
    public class MessageDeleteAction : IDataAction<int, bool>
    {
        public MessageDeleteAction(int message)
        {
            Context = message;
        }

        public int Context { get; private set; }

        public bool Execute()
        {
            try
            {
                string thread;
                string topic;
                using (var scope = new UnitOfWorkScope())
                {
                    var repo = scope.UnitOfWork.RepositoryFor<Message>();
                    var msg = repo.First(x => x.Id == Context);
                    thread = msg.Thread.Slug;
                    topic = msg.Thread.Topic.Slug;
                    repo.Delete(msg);

                    scope.Commit();
                }
                CacheKeyStore.ExpireWithType<ThreadListViewData.Thread>(topic);
                new MessageListCache(thread).Expire();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}