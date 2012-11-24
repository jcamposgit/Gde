﻿#region license

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

using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ClubStarterKit.Infrastructure.Routing
{
    public class LegacyHandler : MvcHandler
    {
        public LegacyHandler(RequestContext requestContext)
            : base(requestContext)
        {
        }

        protected override void ProcessRequest(HttpContextBase httpContext)
        {
            string redirectActionName = ((LegacyRoute) RequestContext.RouteData.Route).RedirectUrl;

            VirtualPathData data = RouteTable.Routes.GetVirtualPath(httpContext.Request.RequestContext,
                                                                    redirectActionName,
                                                                    httpContext.Request.RequestContext.RouteData.Values);
            httpContext.Response.Status = "301 Moved Permanently";
            httpContext.Response.AppendHeader("Location", data.VirtualPath);
        }
    }
}