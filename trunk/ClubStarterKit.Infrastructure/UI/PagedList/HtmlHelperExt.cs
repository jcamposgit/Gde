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
using ClubStarterKit.Core;
using ClubStarterKit.Infrastructure.UI.PagedList;

namespace System.Web.Mvc
{
    public static class HtmlHelperPagerExt
    {
        public static MvcHtmlString Pager<T>(this HtmlHelper<T> html, string action = "", string controller = "", string pageRouteValue = "page", RouteValueDictionary routeValues = null, int maxNumberedPages = 5, string prefix = "Page")
            where T : IPagedList
        {
            return html.Pager(html.ViewData.Model, action, controller, pageRouteValue, routeValues, maxNumberedPages, prefix);
        }

        public static MvcHtmlString Pager(this HtmlHelper html, IPagedList list, 
                                          string action = "", string controller = "", 
                                          string pageRouteValue = "page", RouteValueDictionary routeValues = null, 
                                          int maxNumberedPages = 5, string prefix = "Page")
        {
            if (string.IsNullOrEmpty(controller))
                controller = html.ViewContext.RequestContext.RouteData.Values["controller"].ToString();

            if (string.IsNullOrEmpty(action))
                action = html.ViewContext.RequestContext.RouteData.Values["action"].ToString();

            var pagedHtml = new PagerList(list, true, maxNumberedPages).RenderHtml(html.UrlHelper(), action, controller, pageRouteValue, routeValues, prefix);

            return MvcHtmlString.Create(pagedHtml);
        }
    }
}