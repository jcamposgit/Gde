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

using System.Collections.Generic;
using System.Linq;
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure.Cache;
using ClubStarterKit.Web.ViewData.Forum;

namespace ClubStarterKit.Web.Infrastructure.Forum
{
    public class TopicListCache : HttpCacheBase<IEnumerable<TopicGroupItem>>
    {
        public override string ContentType
        {
            get { return "TopicList"; }
        }

        protected override IEnumerable<TopicGroupItem> Grab()
        {
            IEnumerable<TopicGroupItem> output = null;

            using (var scope = new UnitOfWorkScope())
            {
                IList<TopicViewItem> topics = (from t in scope.UnitOfWork.RepositoryFor<Topic>()
                                               orderby t.Position ascending
                                               select new TopicViewItem
                                               {
                                                   Description = t.Description,
                                                   HasUnread = false, // TODO: find from profile
                                                   LastUpdate = t.LastUpdated,
                                                   Title = t.Title,
                                                   TotalThreads = t.ThreadCount,
                                                   Group = t.TopicGroup,
                                                   TopicSlug = t.Slug,
                                                   Id = t.Id,
                                                   VisibleToAnonymous = t.VisibleToAnonymous
                                               }).ToList();

                output = from topic in topics
                         group topic by topic.Group into grouped
                         select new TopicGroupItem
                         {
                             Group = grouped.Key,
                             Topics = grouped
                         };
                scope.Commit();
            }

            return output;
        }
    }
}