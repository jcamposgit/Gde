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

using System.Linq;
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure.Cache;
using ClubStarterKit.Web.ViewData.Forum;

namespace ClubStarterKit.Web.Infrastructure.Forum
{
    public class MessageListAction : IDataAction<string, MessageListViewData>
    {
        public MessageListAction(string thread)
        {
            Context = thread;
        }

        public string Context { get; private set; }

        public MessageListViewData Execute()
        {
            return new MessageListCache(Context).CachedValue;
        }
    }

    public class MessageListCache : HttpCacheBase<MessageListViewData>
    {
        public MessageListCache(string thread)
            : base()
        {
            Thread = thread;
        }

        public string Thread { get; private set; }
        public override string ContentType
        {
            get { return "MessageList_" + Thread; }
        }

        protected override MessageListViewData Grab()
        {
            var viewData = new MessageListViewData();

            using (var scope = new UnitOfWorkScope())
            {
                var query = from m in scope.UnitOfWork.RepositoryFor<Message>().With(x => x.User)
                            where !m.Hidden && m.Thread.Slug == Thread
                            orderby m.SubmissionDate ascending
                            select m;

                // load from db and convert
                viewData.Messages = from m in query.ToList()
                                    select new MessageViewItem
                                    {
                                        DateAdded = m.SubmissionDate,
                                        Body = m.Body,
                                        Member = m.User,
                                        Id = m.Id
                                    };

                var thread = scope.UnitOfWork.RepositoryFor<Thread>().First(th => th.Slug == Thread);
                viewData.Locked = thread.Locked;
                viewData.ThreadSlug = Thread;
                viewData.Title = thread.Title;
                
                scope.Commit();
            }

            return viewData;
        }
    }

}