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
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure;
using ClubStarterKit.Infrastructure.Cache;
using StructureMap;

namespace ClubStarterKit.Web.Infrastructure.News
{
    public class NewsRssAction : IDataAction<UrlHelper, SyndicationFeed>
    {
        public NewsRssAction(UrlHelper helper)
        {
            Config = ObjectFactory.GetInstance<ISiteConfigProvider>();
            Context = helper;
        }

        #region Implementation of IDataAction<Uri,SyndicationFeed>

        public UrlHelper Context { get; private set; }
        public ISiteConfigProvider Config { get; private set; }
        public SyndicationFeed Execute()
        {
            IList<SyndicationItem> items = new List<SyndicationItem>();

            using (var scope = new UnitOfWorkScope())
            {
                new CollectionDataCache<Announcement>().Only(announcement => announcement.ItemDate <= DateTime.Now)
                                                       .LoadedBy(scope.UnitOfWork)
                                                       .CachedValue
                                                       .Foreach(item =>
                {
                    var synd = new SyndicationItem(item.Title,
                                                   item.Description,
                                                   new Uri(Context.AbsoluteAction(Website.News.Show(item.Slug))),
                                                   item.Id.ToString(), item.ItemDate);
                    if(item.Owner != null)
                        synd.Authors.Add(new SyndicationPerson(item.Owner.Email, item.Owner.FullName(),
                                                            Context.AbsoluteAction(Website.Blog.Author(item.Owner.Slug, null))));
                    synd.PublishDate = item.ItemDate;
                    items.Add(synd);
                });
            }

            var feed = new SyndicationFeed(Config.ApplicationName + " News", "News for " + Config.ApplicationName,
                                           new Uri(Context.AbsoluteAction(Website.News.Rss())), items);
            return feed;
        }

        #endregion
    }
}