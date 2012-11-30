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

namespace ClubStarterKit.Web.Infrastructure.Forum
{
    public class ThreadLockChangeAction : IDataAction<ThreadLockChangeAction.LockChangeContext, bool>
    {
        public class LockChangeContext
        {
            public bool Lock { get; set; }
            public string ThreadSlug { get; set; }
        }

        public ThreadLockChangeAction(string threadSlug, bool @lock)
        {
            Context = new LockChangeContext
            {
                ThreadSlug = threadSlug,
                Lock = @lock
            };
        }

        public LockChangeContext Context { get; private set; }

        public bool Execute()
        {
            try
            {
                string topic;
                using (var scope = new UnitOfWorkScope())
                {
                    var repo = scope.UnitOfWork.RepositoryFor<Thread>();

                    var th = repo.First(x => x.Slug == Context.ThreadSlug);
                    th.Locked = Context.Lock;
                    
                    topic = th.Topic.Slug;
                    
                    repo.Save(th);
                    

                    scope.Commit();
                }

                new MessageListCache(Context.ThreadSlug).Expire();
                CacheKeyStore.ExpireWithType<Thread>(Context.ThreadSlug);
                CacheKeyStore.ExpireWithType<ViewData.Forum.ThreadListViewData.Thread>(topic);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}