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
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Infrastructure;
using StructureMap;

namespace ClubStarterKit.Web.Infrastructure.Forum
{
    public class TopicRssAction : IDataAction<string, SyndicationFeed>
    {
        public TopicRssAction(UrlHelper helper, string topic)
        {
            Config = ObjectFactory.GetInstance<ISiteConfigProvider>();
            Context = topic;
            Url = helper;
        }

        #region Implementation of IDataAction<Uri,SyndicationFeed>

        public string Context { get; private set; }
        public ISiteConfigProvider Config { get; private set; }
        public UrlHelper Url { get; set; }

        public SyndicationFeed Execute()
        {
            var cache = new ThreadListAction(0, Context).Execute();
            var items = from item in cache.Threads
                        select new SyndicationItem(item.Title, "", new Uri(Url.AbsoluteAction(Website.Thread.ViewThread(item.ThreadSlug))))
                        {
                            PublishDate = item.LastUpdated ?? DateTimeOffset.Now
                        };

            var feed = new SyndicationFeed(Config.ApplicationName + " Forum Threads for Topic " + cache.Topic.Title, cache.Topic.Description,
                                           new Uri(Url.AbsoluteAction(Website.Forum.Rss(Context))), items);

            feed.Categories.Add(new SyndicationCategory(cache.Topic.Group));
            return feed;
        }

        #endregion
    }
}