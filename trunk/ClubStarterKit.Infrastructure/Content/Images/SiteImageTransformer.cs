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

using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ClubStarterKit.Infrastructure.Application;
using ClubStarterKit.Infrastructure.Content.Css;
using ClubStarterKit.Infrastructure.Ext;

namespace ClubStarterKit.Infrastructure.Content.Images
{
    public class SiteImageTransformer : ICssTransformer
    {
        #region ICssTransformer Members

        public string Transform(HttpContextBase httpContext, string css, IApplicationIdProvider applicationId)
        {
            // some setup code
            var urlHelper = new UrlHelper(new RequestContext(httpContext, RouteTable.Routes.GetRouteData(httpContext)));
            // format with one param (filename)
            string imageUrlFormat =
                HttpUtility.UrlDecode(httpContext.BaseUrl().TrimEnd('/') +
                                      urlHelper.RouteUrl(ContentConstants.SiteImageRouteName, new { applicationId = applicationId.ApplicationId, file = "{0}" }));

            // iterate over images to find the replacements
            new ImageContentAggrigator(httpContext, applicationId).CachedValue.ForEach(image =>
               {
                   string imageUrl = string.Format(imageUrlFormat, image.ToString().ToLower());
                   var currentFormat = Format(image, imageUrl);
                   css = css.Replace(currentFormat.Item1, currentFormat.Item2);
               });
            return css;
        }

        #endregion

        protected virtual Tuple<string, string> Format(WebsiteImage image, string imageUrl)
        {
            return new Tuple<string, string>("url(" + image.ToString() + ");", "url(" + imageUrl + ");");
        }
    }
}