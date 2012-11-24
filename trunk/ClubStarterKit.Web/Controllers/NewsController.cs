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
using System.Web.Mvc;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure;
using ClubStarterKit.Infrastructure.ActionResults;
using ClubStarterKit.Web.Infrastructure.Membership;
using ClubStarterKit.Web.Infrastructure.News;
using ClubStarterKit.Web.Infrastructure.Membership.Identification;

namespace ClubStarterKit.Web.Controllers
{
    public partial class NewsController : BaseController
    {
        public virtual ActionResult Index()
        {
            ViewData.Title("News");
            SetRss();
            return View(Views.List, new NewsItemListAction(0).Execute());
        }

        protected virtual void SetRss()
        {
            this.Rss(Website.News.Rss(), "News");
        }

        public virtual ActionResult List(int id = 1)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException("id", id, "Value of page cannot be lower than 1");

            ViewData.Title("Past News (Page " + id + ")");
            SetRss();

            return AjaxTemplatedResult(Views.DisplayTemplates.PagedNews, Views.List, new NewsItemListAction(id - 1).Execute());
        }

        public virtual ActionResult Rss()
        {
            return new RssResult(new NewsRssAction(Url).Execute());
        }

        [ActionName("View")]
        public virtual ActionResult Show(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction(Index());

            var item = new NewsItemRetrievalAction(id).Execute();

            if (item == null)
                throw new Exception(string.Format("The news item with id {0} was not found.", id));

            ViewData.Title("News - " + item.Title);
            return View(Views.View, item);
        }

        [Admin]
        public virtual ActionResult Edit(string id)
        {
            var post = new NewsItemRetrievalAction(id).Execute();

            if (post == null)
                throw new ArgumentException(string.Format("News item with id {0} was not found", id));

            ViewData.Title("News Item Edit - " + post.Title);
            return View(Views.Edit, post);
        }

        [Admin]
        public virtual ActionResult New()
        {
            ViewData.Title("New News Item");
            return View(Website.News.Views.Edit, new Announcement { ItemDate = DateTime.Now });
        }

        [ValidateInput(false), Admin, HttpPost]
        public virtual ActionResult Update(Announcement post)
        {
            if (post == null)
                throw new ArgumentNullException("post");

            post.Owner = UserRetrieval.GetUser(HttpContext).Return();

            var result = new NewsItemUpdateAction(post).Execute();

            if (result)
                return RedirectToAction(Website.News.Show(post.Slug));

            return RedirectToAction(Website.News.Edit(post.Slug));
        }

        [Admin]
        public virtual ActionResult Delete(string id, string returnurl = "")
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("post");

            new NewsItemDeleteAction(id).Execute();

            if (string.IsNullOrEmpty(returnurl))
                return RedirectToAction(Website.News.Index());
            return Redirect(returnurl);
        }
    }
}