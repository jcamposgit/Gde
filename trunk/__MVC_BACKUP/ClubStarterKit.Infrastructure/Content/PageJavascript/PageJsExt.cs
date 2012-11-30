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
    public static class PageJsExt
    {
        /// <summary>
        /// Link to a "page javascript" file
        /// </summary>
        /// <param name="html"></param>
        /// <param name="file">Page javascript to link to</param>
        /// <returns>Script tag for given page javascript</returns>
        public static MvcHtmlString PageJavascript(this HtmlHelper html, string file)
        {
            var jsTag = new TagBuilder("script");
            jsTag.Attributes.Add("type", ContentConstants.JavascriptContentType);

            // setup link
            string url = html.UrlHelper().RouteUrl(ContentConstants.PageJavascriptRouteName,
                                                   new
                                                   {
                                                       applicationId = html.ApplicationId(),
                                                       page = file.Replace(".js", "")
                                                   });
            jsTag.Attributes.Add("src", url);
            return MvcHtmlString.Create(jsTag.ToString());
        }
    }
}
