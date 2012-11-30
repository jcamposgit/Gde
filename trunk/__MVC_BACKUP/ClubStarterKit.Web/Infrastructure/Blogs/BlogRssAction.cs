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
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure;
using ClubStarterKit.Infrastructure.Cache;
using StructureMap;

namespace ClubStarterKit.Web.Infrastructure.Blogs
{
    public class BlogRssAction : IDataAction<UrlHelper, SyndicationFeed>
    {
        public BlogRssAction(UrlHelper helper, string author = "")
        {
            Config = ObjectFactory.GetInstance<ISiteConfigProvider>();
            Context = helper;
            Author = author;
        }

        #region Implementation of IDataAction<UrlHelper,SyndicationFeed>

        public UrlHelper Context { get; private set; }
        public ISiteConfigProvider Config { get; private set; }
        public string Author { get; private set; }

        public SyndicationFeed Execute()
        {
            return new SyndicationFeed(Title, Title, RssAction, Posts.Select(GetRssItem));
        }

        private SyndicationItem GetRssItem(BlogPost post)
        {
            var posturi = new Uri(Context.AbsoluteAction(Website.Blog.Show(post.Slug)));
            var synd = new SyndicationItem(post.Title, post.Content, posturi, post.Id.ToString(), post.PostDate);
            
            if (post.Author != null)
                synd.Authors.Add(new SyndicationPerson(post.Author.Email, 
                                                       post.Author.FullName(),
                                                       Context.AbsoluteAction(Website.Blog.Author(post.Author.Slug, null))));

            return synd;
        }

        private bool IsAuthor
        {
            get
            {
                return !string.IsNullOrEmpty(Author);
            }
        }

        private IEnumerable<BlogPost> Posts
        {
            get 
            {
                var cache = new CollectionDataCache<BlogPost>().With(x => x.With(p => p.Author));

                if (IsAuthor)
                    cache = cache.Only(post => post.Author.Slug == Author);

                return cache.CachedValue;
            }
        }

        private string Title
        {
            get 
            {
                string title = Config.ApplicationName + " Blog";
                return IsAuthor ? title + " Posts by " + Author : title;
            }
        }

        private Uri RssAction
        {
            get
            {
                if (IsAuthor)
                    return new Uri(Context.AbsoluteAction(Website.Blog.Rss(Author)));
                return new Uri(Context.AbsoluteAction(Website.Blog.Rss()));
            }
        }

        #endregion
    }
}