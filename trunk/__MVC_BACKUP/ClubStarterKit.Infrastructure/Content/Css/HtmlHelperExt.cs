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

using ClubStarterKit.Infrastructure.Content;

namespace System.Web.Mvc
{
    public static class HtmlHelperCssExt
    {
        /// <summary>
        /// Adds link to a page's head for the website's CSS
        /// </summary>
        /// <param name="html"></param>
        /// <returns>CSS link</returns>
        public static MvcHtmlString Css(this HtmlHelper html)
        {
            var cssTag = new TagBuilder("link");
            cssTag.Attributes.Add("type", "text/css");
            cssTag.Attributes.Add("rel", "stylesheet");

            // setup link
            string url = html.UrlHelper().RouteUrl(ContentConstants.CssRouteName,
                                                   new {applicationId = html.ApplicationId()});
            cssTag.Attributes.Add("href", url);

            return MvcHtmlString.Create(cssTag.ToString());
        }
    }
}