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
using ClubStarterKit.Web.ViewData.Forum;

namespace ClubStarterKit.Web.Controllers
{
    /// <summary>
    /// Contorller for TOPICS
    /// </summary>
    public partial class ForumController : BaseController
    {
        public virtual ActionResult Index()
        {
            ViewData.Title("Forum");
            return View(Views.TopicList, new TopicListAction().Execute());
        }

        [ActionName("View")]
        public virtual ActionResult ViewTopic(string id, int? page = null)
        {
            var model = new ThreadListAction((page ?? 1) - 1, id).Execute();

            return new AjaxDeterministicResult
            (
                () => View(Views.DisplayTemplates.WithPath.ThreadList, model),
                () => 
                    {
                        ViewData.Title("Forum - Topic -" + model.Topic.Title);
                        this.Rss(Website.Forum.Rss(id), "Threads for Forum Topic " + model.Topic.Title);
                        return View(Views.Threads, model);
                    }
            );
        }

        public virtual ActionResult Rss(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            return new RssResult(new TopicRssAction(Url, id).Execute());        
        }

        [Admin]
        public virtual ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            ViewData.Title("Edit Topic");
            var viewData = new EditTopicViewData(new TopicRetrievalAction(id).Execute());
            return View(Views.EditTopic, viewData);
        }

        [Admin]
        public virtual ActionResult New()
        {
            ViewData.Title("New Topic");
            return View(Views.EditTopic, new EditTopicViewData());
        }

        [Admin, HttpPost]
        public virtual ActionResult Update(EditTopicViewData model)
        {
            if (new TopicUpdateAction(model).Execute())
                return RedirectToAction(Website.Forum.Index());
            return View(Views.EditTopic, model);
        }

        [Admin]
        public virtual ActionResult Delete(string id)
        { 
            if(string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");
            var result = new TopicDeleteAction(id).Execute();

            return RedirectToAction(Website.Forum.Index());
        }
    }
}