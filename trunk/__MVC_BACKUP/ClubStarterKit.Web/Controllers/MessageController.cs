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

namespace ClubStarterKit.Web.Controllers
{
    public partial class MessageController : BaseController
    {
        [Authorize] // TODO: ASYNC controller?
        public virtual ActionResult MarkSpam(int id)
        {
            var user = UserRetrieval.GetUser(HttpContext);

            if (user.IsNothing)
                throw new Exception("User not found");

            var success = new MarkMessageSpamAction(user.Return(), id, Url).Execute();

            return Json(new { success });
        }

        [Admin]
        public virtual ActionResult Delete(int id)
        {
            var success = new MessageDeleteAction(id).Execute();

            return new AjaxDeterministicResult
            (
                () => Json(new { success }),
                () => RedirectToAction(Website.Forum.Index())
            );
        }

        [Authorize, HttpPost, ValidateInput(false)]
        public virtual ActionResult Update(string message, string thread, int messageId)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            var user = UserRetrieval.GetUser(HttpContext);
            if (user.IsNothing)
                throw new Exception("User not found");

            var result = new MessageUpdateAction(message, thread, messageId, user.Return()).Execute();
            
            return new AjaxDeterministicResult
            (
                () => View(Website.Shared.Views.DisplayTemplates.WithPath.ForumMessageList, 
                           new MessageListAction(thread).Execute().Messages),
                () => RedirectToAction(Website.Thread.ViewThread(thread))
            );
        }
    }
}
