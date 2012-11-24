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
using System.Web;
using System.Web.Mvc;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure;
using ClubStarterKit.Web.Infrastructure.Content;
using ClubStarterKit.Web.Infrastructure.Membership;

namespace ClubStarterKit.Web.Controllers
{
    public partial class ContentPageController : BaseController
    {
        public virtual ActionResult Page(string id)
        {
            if (string.IsNullOrEmpty(id))
                HandleUnknownPage(id);

            var page = new ContentPageRetrievalAction(id).Execute();

            if (page == null)
                HandleUnknownPage(id);

            ViewData.Title(page.PageTitle);
            return View(Views.Page, page);
        }

        [Admin]
        public virtual ActionResult Edit(string id)
        {
            var page = new ContentPageRetrievalAction(id).Execute();
            ViewData.Title("Edit Page - " + page.PageTitle);
            return View(Views.Edit, page);
        }

        [Admin]
        public virtual ActionResult New()
        {
            ViewData.Title("New Page");
            return View(Views.Edit, new ContentPage());
        }

        [Admin]
        public virtual ActionResult Update(ContentPage page)
        {
            if (page == null)
                throw new ArgumentNullException("page");

            var result = new ContentPageUpdateAction(page).Execute();

            if (result)
                return RedirectToAction(Website.ContentPage.Page(page.PageUrl));

            return View(Views.Edit, page);
        }

        [Admin]
        public virtual ActionResult Delete(string id)
        {
            if(string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            new ContentPageDeleteAction(id).Execute();

            return RedirectToAction(Website.Home.Index());
        }

        private static void HandleUnknownPage(string page)
        {
            throw new HttpException(404, string.Format("The page '{0}' cannot be found.", page));
        }
    }
}