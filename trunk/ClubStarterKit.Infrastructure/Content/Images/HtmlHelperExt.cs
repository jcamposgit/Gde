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
using ClubStarterKit.Infrastructure.Content;

namespace System.Web.Mvc
{
    public static class HtmlHelperImgExt
    {
        /// <summary>
        /// Outputs an img tag with the specifed image name from the /Content/Images/ directory
        /// </summary>
        /// <param name="html"></param>
        /// <param name="image">Image name</param>
        /// <param name="htmlValues"></param>
        /// <returns></returns>
        public static MvcHtmlString SiteImage(this HtmlHelper html, string image, object htmlValues = null)
        {
            var tag = new TagBuilder("img");
            tag.Attributes.Add("alt", image);
            tag.Attributes.Add("src", html.UrlHelper().SiteImage(image));

            if (htmlValues != null)
                tag.MergeAttributes(new RouteValueDictionary(htmlValues));
            return MvcHtmlString.Create(tag.ToString());
        }

        /// <summary>
        /// String URL of a site image
        /// </summary>
        /// <param name="url"></param>
        /// <param name="image">Image name</param>
        /// <returns>Relative URL of the image</returns>
        public static string SiteImage(this UrlHelper url, string image)
        {
            return url.RouteUrl
                (
                    ContentConstants.SiteImageRouteName,
                    new
                    {
                        file = image,
                        applicationId = url.ApplicationId()
                    }
                );
        }
    }
}