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

using System.Linq;
using ClubStarterKit.Core;
using ClubStarterKit.Infrastructure.Routing;

namespace System.Web.Mvc
{
    public static class PagerHelper
    {
        /// <summary>
        /// Pager for a paged list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html"></param>
        /// <param name="fn_result">Value of the Controller/Action given a page number</param>
        /// <param name="maxNumberedPages"></param>
        /// <param name="prefix">Prefix of the hash values</param>
        /// <returns>Pager links</returns>
        public static MvcHtmlString Pager<T>(this HtmlHelper<T> html, Func<int, ActionResult> fn_result, int maxNumberedPages = 5, string prefix = "Page")
            where T : IPagedList
        {
            return html.Pager(html.ViewData.Model, fn_result, maxNumberedPages, prefix);
        }

        /// <summary>
        /// Pager for a paged list
        /// </summary>
        /// <param name="html"></param>
        /// <param name="list">PagedList used to generate page links</param>
        /// <param name="fn_result">Value of the Controller/Action given a page number</param>
        /// <param name="maxNumberedPages"></param>
        /// <param name="prefix">Prefix of the hash values</param>
        /// <returns>Pager links</returns>
        public static MvcHtmlString Pager(this HtmlHelper html, IPagedList list, Func<int, ActionResult> fn_result, int maxNumberedPages = 5, string prefix = "Page")
        {
            int pageconst = -123454321;
            var result = fn_result(pageconst) as IT4MVCActionResult;

            if (result == null)
                throw new Exception("ActionResult must be an IT4MVCActionResult");

            var pageRouteValue = result.RouteValues.Where(x => x.Value.Equals(pageconst)).FirstOrDefault().Key;
            var extraRouteValues = result.RouteValues.ExtractValues(new string[] { "controller", "action", pageRouteValue});
            return html.Pager(list, result.Action, result.Controller, pageRouteValue, extraRouteValues, maxNumberedPages, prefix);
        }
    }
}