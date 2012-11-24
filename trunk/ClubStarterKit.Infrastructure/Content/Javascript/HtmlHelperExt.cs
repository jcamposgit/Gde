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
    public static class HtmlHelperJsExt
    {
        /// <summary>
        /// Javascript link for all the website's main javascript files
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static MvcHtmlString JavaScript(this HtmlHelper html)
        {
            var jsTag = new TagBuilder("script");
            jsTag.Attributes.Add("type", ContentConstants.JavascriptContentType);

            // setup link
            string url = html.UrlHelper().RouteUrl(ContentConstants.JavascriptRouteName,
                                                   new { applicationId = html.ApplicationId() });
            jsTag.Attributes.Add("src", url);

            return MvcHtmlString.Create(jsTag.ToString());
        }
    }
}