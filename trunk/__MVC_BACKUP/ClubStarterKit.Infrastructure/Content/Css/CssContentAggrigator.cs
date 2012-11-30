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

using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using ClubStarterKit.Infrastructure.Application;
using ClubStarterKit.Infrastructure.Cache;

namespace ClubStarterKit.Infrastructure.Content.Css
{
    /// <summary>
    /// Aggrigator of the CSS content in a website's /Content/Css/ directory
    /// </summary>
    public class CssContentAggrigator : HttpCacheBase<string>, IContentAggregator
    {
        /// <summary>
        /// Aggrigator of the CSS content in a website's Content/Css/ directory
        /// </summary>
        /// <param name="context"></param>
        /// <param name="applicationId"></param>
        public CssContentAggrigator(HttpContextBase context, IApplicationIdProvider applicationId)
            : base(context, applicationId)
        {
        }

        /// <summary>
        /// Aggrigator of the CSS content in a website's 'Content/Css/' directory
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cache"></param>
        /// <param name="applicationId"></param>
        public CssContentAggrigator(HttpContextBase context, CacheBase cache, IApplicationIdProvider applicationId)
            : base(context, cache, applicationId)
        {
        }

        #region IContentAggregator Members

        public override string ContentType
        {
            get { return "Css"; }
        }

        public ActionResult ContentResult
        {
            get
            {
                return new ContentResult
                           {
                               Content = CachedValue,
                               ContentType = "text/css"
                           };
            }
        }

        #endregion

        protected override string Grab()
        {
            string cssDirectory = SiteContentUtils.GetFiles(ContentType, "css")
                .ToStringArray()
                .ConcatAll();

            return CssMinifier.Minify(Transform(cssDirectory));
        }

        protected virtual string Transform(string css)
        {
            string currentCss = css;
            new BinPartLoader<ICssTransformer>().Items.Foreach
                (
                    transformer => currentCss = transformer.Transform(HttpContext, currentCss, ApplicationIdProvider)
                );

            return currentCss;
        }
    }
}