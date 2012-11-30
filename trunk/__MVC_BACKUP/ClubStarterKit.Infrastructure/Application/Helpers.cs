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

using System.Web.Routing;
using ClubStarterKit.Infrastructure.Application;
using ClubStarterKit.Infrastructure.Ext;
using StructureMap;

namespace System.Web.Mvc
{
    public static class Helpers
    {
        /// <summary>
        /// Current application ID
        /// </summary>
        /// <param name="url"></param>
        /// <returns>String-based application ID</returns>
        public static string ApplicationId(this UrlHelper url)
        {
            return ObjectFactory.GetInstance<IApplicationIdProvider>().ApplicationId;
        }

        /// <summary>
        /// Current application ID
        /// </summary>
        /// <param name="html"></param>
        /// <returns>String-based application ID</returns>
        public static string ApplicationId(this HtmlHelper html)
        {
            return html.UrlHelper().ApplicationId();
        }

        /// <summary>
        /// Full URL for a controller action
        /// </summary>
        /// <param name="url"></param>
        /// <param name="action">Controller action</param>
        /// <param name="controller">Controller</param>
        /// <param name="routeValues">Extra route values</param>
        /// <remarks>
        /// If the URL helper is null, an empty string is always returned
        /// </remarks>
        /// <returns>Full URL of the controller action</returns>
        public static string AbsoluteAction(this UrlHelper url, string action, string controller, RouteValueDictionary routeValues)
        {
            if (url == null) 
                return string.Empty;

            return url.RequestContext.HttpContext.BaseUrl().TrimEnd('/') + url.Action(action, controller, routeValues);
        }
    }
}