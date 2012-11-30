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
    public class ThreadRssAction : IDataAction<string, SyndicationFeed>
    {
        public ThreadRssAction(UrlHelper helper, string thread)
        {
            Config = ObjectFactory.GetInstance<ISiteConfigProvider>();
            Context = thread;
            Url = helper;
        }

        #region Implementation of IDataAction<Uri,SyndicationFeed>

        public string Context { get; private set; }
        public ISiteConfigProvider Config { get; private set; }
        public UrlHelper Url { get; set; }

        public SyndicationFeed Execute()
        {
            var cache = new MessageListCache(Context).CachedValue;
            var items = cache.Messages.Select(item =>
            {
                var synd = new SyndicationItem("POST by " + item.Member.FullName(),
                                               item.Body,
                                               new Uri(Url.AbsoluteAction(Website.Thread.ViewThread(Context)) + "#" + item.Id),
                                               item.Id.ToString(), 
                                               item.DateAdded);

                if (item.Member != null)
                    synd.Authors.Add(new SyndicationPerson(item.Member.Email, item.Member.FullName(),
                                                           Url.AbsoluteAction(Website.Membership.Profile(item.Member.Username))));
                synd.PublishDate = item.DateAdded;
                return synd;
            });

            return new SyndicationFeed(Config.ApplicationName + " Forum Thread " + cache.Title, "Forum messages for thread " + cache.Title,
                                           new Uri(Url.AbsoluteAction(Website.Thread.Rss(Context))), items);
        }

        #endregion
    }
}