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

using ClubStarterKit.Infrastructure.Routing;

namespace System.Web.Mvc
{
    public static class UrlHelperExt
    {
        /// <summary>
        /// Full URL for a given controller action
        /// </summary>
        /// <param name="url"></param>
        /// <param name="action">Controller Action for the URL</param>
        /// <returns>Url for the controller action wtih the base URL</returns>
        public static string AbsoluteAction(this UrlHelper url, ActionResult action)
        { 
            var t4Action = action as IT4MVCActionResult;
            var extraRouteValues = t4Action.RouteValues.ExtractValues(new string[] { "controller", "action"});
            return url.AbsoluteAction(t4Action.Action, t4Action.Controller, extraRouteValues);
        }
    }
}