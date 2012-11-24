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
using ClubStarterKit.Infrastructure;
using ClubStarterKit.Infrastructure.ActionResults;
using ClubStarterKit.Web.Infrastructure.Forum;
using ClubStarterKit.Web.Infrastructure.Membership;
using ClubStarterKit.Web.Infrastructure.Membership.Identification;
using ClubStarterKit.Web.ViewData.Forum;

namespace ClubStarterKit.Web.Controllers
{
    public partial class ThreadController : BaseController
    {
        [ActionName("View")]
        public virtual ActionResult ViewThread(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentOutOfRangeException("id");
            var model = new MessageListAction(id).Execute();

            ViewData.Title("Forum - Thread - " + model.Title);

            this.Rss(Website.Thread.Rss(id), "Forum Thread " + model.Title + " Messages");

            return View(Views.View, model);
        }

        [Authorize]
        public virtual ActionResult New(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            ViewData.Title("New Forum Post");
            return View(Views.NewThread, new NewThreadViewData
            {
                TopicSlug = id
            });
        }

        [Authorize, HttpPost, ValidateInput(false)]
        public virtual ActionResult Add(string id, string message, string title)
        {
            var user = UserRetrieval.GetUser(HttpContext);

            if (user.IsNothing)
                throw new Exception("User not found");

            var result = new ThreadAddAction(message, title, id, user.Return()).Execute();

            if (string.IsNullOrEmpty(result))
                return RedirectToAction(Website.Thread.New(id));

            return RedirectToAction(Website.Thread.ViewThread(result));

        }

        public virtual ActionResult Rss(string id)
        {
            return new RssResult(new ThreadRssAction(Url, id).Execute());
        }

        [Admin]
        public virtual ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            var result = new ThreadDeleteAction(id).Execute();

            return new AjaxDeterministicResult
            (
                () => Json(new { success = !string.IsNullOrEmpty(result) }),
                () => RedirectToAction(Website.Forum.ViewTopic(result, null))
            );
        }

        [Admin]
        public virtual ActionResult Lock(string id)
        {
            new ThreadLockChangeAction(id, true).Execute();
            return RedirectToAction(Website.Thread.ViewThread(id));
        }

        [Admin]
        public virtual ActionResult Unock(string id)
        {
            new ThreadLockChangeAction(id, false).Execute();
            return RedirectToAction(Website.Thread.ViewThread(id));
        }
    }
}