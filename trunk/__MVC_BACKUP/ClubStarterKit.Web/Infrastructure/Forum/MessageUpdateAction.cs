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

namespace ClubStarterKit.Web.Infrastructure.Forum
{
    public class MessageUpdateAction : IDataAction<MessageUpdateAction.MessageUpdateContext, bool>
    {
        public class MessageUpdateContext
        {
            public string Message { get; set; }
            public int MessageId { get; set; }
            public string Thread { get; set; }
            public User User { get; set; }
        }

        public MessageUpdateAction(string message, string thread, int messageId, User user)
        {
            Context = new MessageUpdateContext
            {
                Message = message,
                Thread = thread,
                MessageId = messageId,
                User = user
            };
        }

        public MessageUpdateContext Context { get; private set; }

        public bool Execute()
        {
            try
            {
                if(Context.MessageId > 0)
                {
                    using (var scope = new UnitOfWorkScope())
                    {
                        scope.UnitOfWork.RepositoryFor<Message>().Update(new ExpressionValuePair<Message, string>(x=>x.Body, Context.Message),
                                                                         new ExpressionValuePair<Message, int>(x=>x.Id, Context.MessageId));
                        scope.Commit();
                    }
                }
                else
                {
                    using (var scope = new UnitOfWorkScope())
                    {
                        var thread = scope.UnitOfWork.RepositoryFor<Thread>().First(x=>x.Slug == Context.Thread);
                        var message = new Message
                        {
                             Body = Context.Message,
                             Hidden = false,
                             SubmissionDate = DateTimeOffset.Now,
                             Thread = thread,
                             User = Context.User
                        };
                        scope.UnitOfWork.RepositoryFor<Message>().Save(message);
                        
                        scope.Commit();
                    }
                }

                new MessageListCache(Context.Thread).Expire();
                CacheKeyStore.ExpireWithType<Thread>(Context.Thread);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}