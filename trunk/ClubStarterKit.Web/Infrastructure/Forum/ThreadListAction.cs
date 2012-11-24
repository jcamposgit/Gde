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
using ClubStarterKit.Core;
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure.Cache;
using ClubStarterKit.Web.ViewData.Forum;

namespace ClubStarterKit.Web.Infrastructure.Forum
{
    public class ThreadListAction : IDataAction<ThreadListAction.ThreadListActionContext, ThreadListViewData>
    {
        public class ThreadListActionContext
        {
            public int Index { get; set; }
            public string Topic { get; set; }
        }

        public ThreadListAction(int index, string topic)
        {
            Context = new ThreadListActionContext
            {
                Index = index,
                Topic = topic
            };
        }

        public ThreadListActionContext Context { get; private set; }

        public ThreadListViewData Execute()
        {
            return new ThreadListViewData
            {
                Threads = new ThreadListCache(Context).CachedValue,
                Topic = new TopicRetrievalAction(Context.Topic).Execute()
            };
        }

        private class ThreadListCache : HttpCacheBase<IPagedList<ThreadListViewData.Thread>>
        {
            public ThreadListCache(ThreadListActionContext context)
            {
                Context = context;
            }
            public ThreadListActionContext Context { get; private set; }

            public override string ContentType
            {
                get { return "ThreadList_" + Context.Topic + "_" + Context.Index; }
            }

            protected override IPagedList<ThreadListViewData.Thread> Grab()
            {
                IPagedList<ThreadListViewData.Thread> pagedList = null;

                using (var scope = new UnitOfWorkScope())
                {
                    var threadQuery = from th in scope.UnitOfWork.RepositoryFor<Thread>()
                                      where th.Topic.Slug == Context.Topic && !th.Hidden
                                      orderby th.DateCreated, th.Title descending
                                      select new ThreadListViewData.Thread
                                      {
                                          LastUpdated = th.LastUpdate,
                                          Locked = th.Locked,
                                          Title = th.Title,
                                          ThreadSlug = th.Slug,
                                          MessageCount = th.MessageCount
                                      };

                    pagedList = threadQuery.ToPagedList(Context.Index, Constants.DefaultPageSize);

                    scope.Commit();
                }

                return pagedList;
            }
        }

    }
}