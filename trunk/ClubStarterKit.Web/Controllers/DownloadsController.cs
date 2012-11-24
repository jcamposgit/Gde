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
using ClubStarterKit.Web.Infrastructure.Downloads;
using ClubStarterKit.Web.Infrastructure.Membership;

namespace ClubStarterKit.Web.Controllers
{
    public partial class DownloadsController : BaseController
    {
        public virtual ActionResult Index()
        {
            ViewData.Title("Downloads List");
            return View(Views.List, new DownloadListAction().Execute());
        }

        public virtual ActionResult Download(int id)
        {
            if(id == 0)
                throw new ArgumentNullException("id");

            var result = new DownloadRetrievalAction(id).Execute();
            return File(Constants.DownloadsFolder + result.Url, result.ContentType, result.Url);
        }

        [Admin]
        public virtual ActionResult New()
        {
            ViewData.Title("New Download");
            return View(Views.Edit);
        }

        [Admin, HttpPost]
        public virtual ActionResult Upload()
        {
            var action = new DownloadUploadAction(HttpContext).Execute();
            return RedirectToAction(Website.Downloads.Index());
        }

        [Admin]
        public virtual ActionResult Delete(int id)
        {
            if (id == 0)
                throw new ArgumentNullException("id");
            var result = new DownloadDeleteAction(id).Execute();
            return RedirectToAction(Website.Downloads.Index());
        }
    }
}