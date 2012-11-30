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
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure.Cache;
using ClubStarterKit.Web.ViewData.Forum;

namespace ClubStarterKit.Web.Infrastructure.Forum
{
    public class TopicUpdateAction : IDataAction<Topic, bool>
    {
        public TopicUpdateAction(EditTopicViewData viewData)
        {
            Context = new Topic
            {
                Id = viewData.Topic.Id,
                Title = viewData.Topic.Title,
                Description = viewData.Topic.Description,
                TopicGroup = viewData.Topic.Group,
                Locked = false, // TODO: enable lock?
                VisibleToAnonymous = viewData.Topic.VisibleToAnonymous,
                Position = 1 // TODO: enable position
            };
        }

        public Topic Context { get; private set; }

        public bool Execute()
        {
            try
            {
                using (var scope = new UnitOfWorkScope())
                {
                    scope.UnitOfWork.RepositoryFor<Topic>().Save(Context);
                    scope.Commit();
                }

                new TopicListCache().Expire();
                CacheKeyStore.ExpireWithType<ThreadListViewData.Thread>(Context.Slug);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}