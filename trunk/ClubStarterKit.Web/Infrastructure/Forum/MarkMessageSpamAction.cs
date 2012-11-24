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
using System.Linq;
using System.Web.Mvc;
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure;
using ClubStarterKit.Web.Infrastructure.Membership.Caches;

namespace ClubStarterKit.Web.Infrastructure.Forum
{
    public class MarkMessageSpamAction : NotifiableDataAction<MarkMessageSpamAction.MarkSpamContext, bool>
    {
        public class MarkSpamContext
        {
            public User Flagger { get; set; }
            public int MessageId { get; set; }
            public UrlHelper Url { get; set; }
        }

        public MarkMessageSpamAction(User user, int message, UrlHelper url)
        {
            Context = new MarkSpamContext
            {
                Flagger = user,
                MessageId = message,
                Url = url
            };
        }

        ///public MarkSpamContext Context { get; private set; }

        protected override IEnumerable<string> Users
        {
            get 
            {
                return new AdminEmailCache().CachedValue;
            }
        }

        protected override string Subject
        {
            get { return "Forum Message Marked as Spam"; }
        }

        protected string contents, viewThreadLink, deleteMessageLink;
        protected override string Body
        {
            get 
            {
                return string.Format(@"A message has been reported as spam by a user. This is the message contents:
                        <br />
                        <br />
                        {0}
                        <br />
                        <a href=""{1}"">View the thread</a>
                        <br />
                        <a href=""{2}"">Delete the message</a>", contents, viewThreadLink, deleteMessageLink);
            }
        }

        protected override bool ExecuteAction()
        {
            try
            {
                string thread;
                using (var scope = new UnitOfWorkScope())
                {
                    var message = scope.UnitOfWork.RepositoryFor<Message>().With(x => x.Thread)
                                                                           .First(x => x.Id == Context.MessageId);

                    thread = message.Thread.Slug;
                    contents = message.Body;
                    viewThreadLink = Context.Url.AbsoluteAction(Website.Thread.ViewThread(thread)) + "#" + message.Id;
                    deleteMessageLink = Context.Url.AbsoluteAction(Website.Message.Delete(message.Id));
                    
                    scope.UnitOfWork.RepositoryFor<SpamFlag>().Save(new SpamFlag
                    {
                        Flagger = Context.Flagger,
                        Message = message
                    });
                    scope.Commit();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}