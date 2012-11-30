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

using System.Security.Principal;
using System.Web.Mvc;
using ClubStarterKit.Core;
using ClubStarterKit.Infrastructure.ActionResults;
using StructureMap;

namespace ClubStarterKit.Infrastructure
{
    public abstract class BaseController : Controller
    {
        private bool _compress = true;

        /// <summary>
        /// Indicator of compression by GZIP or DEFLATE
        /// </summary>
        public bool Compress
        {
            get
            { return _compress; }
            set
            { _compress = value; }
        }

        /// <summary>
        /// User specification for a user to perform administration actions
        /// </summary>
        public virtual ISpecification<IPrincipal> ElevatedPrincipal
        {
            get
            {
                return ObjectFactory.GetInstance<ISpecification<IPrincipal>>();
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (_compress && !ControllerContext.IsChildAction)
                this.Compress();
            ViewData[Constants.ElevatedPermissionViewDataKey] = ElevatedPrincipal;
        }

        /// <summary>
        /// Ajax-deterministic template view result
        /// </summary>
        /// <param name="ajaxDisplayTemplate">Display template to display if the request is an Ajax request</param>
        /// <param name="viewName">View to display if the request is not an Ajax request</param>
        /// <param name="model">Model to push to view</param>
        /// <returns><see cref="AjaxDeterministicResult"/></returns>
        [NonAction]
        public ActionResult AjaxTemplatedResult(string ajaxDisplayTemplate, string viewName, object model)
        {
            return new AjaxDeterministicResult
            (
                () => View("DisplayTemplates/" + ajaxDisplayTemplate, model),
                () => View(viewName, model)
            );
        }
    }
}
