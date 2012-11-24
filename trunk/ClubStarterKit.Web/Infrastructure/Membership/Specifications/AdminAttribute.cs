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
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using ClubStarterKit.Core;
using ClubStarterKit.Infrastructure;

namespace ClubStarterKit.Web.Infrastructure.Membership
{
    public class AdminAttribute : AuthorizeAttribute
    {
        protected BaseController _controller = null;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if(httpContext == null)
                throw new ArgumentNullException("httpContext");

            ISpecification<IPrincipal> spec = new AdminSpecification();

            if(_controller != null && _controller.ElevatedPrincipal != null)
                spec = _controller.ElevatedPrincipal;

            return spec.IsSatisfiedBy(httpContext.User);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            _controller = filterContext.Controller as BaseController;
            filterContext.Controller.ValidateRequest = false;
            base.OnAuthorization(filterContext);
        }
    }
}