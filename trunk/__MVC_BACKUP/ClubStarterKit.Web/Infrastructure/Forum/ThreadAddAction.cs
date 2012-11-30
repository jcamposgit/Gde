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
using System.Linq;
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure.Cache;
using ClubStarterKit.Web.ViewData.Forum;

namespace ClubStarterKit.Web.Infrastructure.Forum
{
    public class ThreadAddAction : IDataAction<ThreadAddAction.MessageContext, string>
    {
        public class MessageContext
        {
            public string Message { get; set; }
            public string Topic { get; set; }
            public string Title { get; set; }
            public User CurrentUser { get; set; }
        }

        public ThreadAddAction(string message, string title, string topic, User user)
        {
            Context = new MessageContext
            {
                Message = message,
                Title = title,
                Topic = topic,
                CurrentUser = user
            };
        }

        public ThreadAddAction.MessageContext Context { get; private set; }

        public string Execute()
        {
            try
            {
                string threadSlug = string.Empty;
                using (var scope = new UnitOfWorkScope())
                {
                    var topic = scope.UnitOfWork.RepositoryFor<Topic>().First(t => t.Slug == Context.Topic);

                    var thread = new Thread
                    {
                        DateCreated = DateTimeOffset.Now,
                        Locked = false,
                        Title = Context.Title,
                        Topic = topic,
                        Hidden = false
                    };
                    
                    scope.UnitOfWork.RepositoryFor<Thread>().Save(thread);

                    var msg = new Message
                    {
                        Body = Context.Message,
                        Hidden = false,
                        SubmissionDate = DateTimeOffset.Now,
                        Thread = thread,
                        User = Context.CurrentUser
                    };

                    scope.UnitOfWork.RepositoryFor<Message>().Save(msg);
                    threadSlug = thread.Slug;
                    //scope.UnitOfWork.TransactionalFlush();
                    scope.Commit();
                }

                CacheKeyStore.ExpireWithType<ThreadListViewData>(Context.Topic);
                new TopicListCache().Expire();
                return threadSlug;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}