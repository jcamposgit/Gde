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
using ClubStarterKit.Web;

namespace System.Web.Mvc
{
    public static class RssHelper
    {
        private const string RssViewDataKey = "RssLinks";

        /// <summary>
        /// Adds an RSS link to the header
        /// </summary>
        /// <param name="c"></param>
        /// <param name="action">Action for the RSS link on the controller</param>
        /// <param name="controller">Controller for the RSS link</param>
        /// <param name="title">RSS Content type</param>
        public static void Rss(this Controller c, ActionResult action, string title)
        {
            var linkTag = new TagBuilder("link");
            linkTag.Attributes.Add("rel", "alternate");
            linkTag.Attributes.Add("type", "application/rss+xml");
            linkTag.Attributes.Add("title", title);
            linkTag.Attributes.Add("href", new UrlHelper(new RequestContext(c.HttpContext, c.RouteData)).Action(action));
            string link = linkTag.ToString();

            if (c.ViewData.ContainsKey(RssViewDataKey))
                c.ViewData[RssViewDataKey] = c.ViewData[RssViewDataKey] + link;
            else
                c.ViewData[RssViewDataKey] = link;
        }

        /// <summary>
        /// RSS links in the view data
        /// </summary>
        /// <param name="viewData"></param>
        /// <returns></returns>
        public static MvcHtmlString Rss(this ViewDataDictionary viewData)
        {
            if (viewData == null || !viewData.ContainsKey(RssViewDataKey))
                return MvcHtmlString.Create(string.Empty);

            return MvcHtmlString.Create(viewData[RssViewDataKey].ToString());
        }

        /// <summary>
        /// Link to an RSS feed
        /// </summary>
        /// <param name="html"></param>
        /// <param name="link">RSS Feed link Controller Action</param>
        /// <returns>Image of an RSS icon with a link to the feed</returns>
        public static MvcHtmlString RssLink(this HtmlHelper html, ActionResult link)
        {
            var url = html.UrlHelper().Action(link);
            var img = html.UrlHelper().SiteImage("rss");
            return MvcHtmlString.Create(string.Format(Constants.RssLink_Html, url, img));
        }
    }
}