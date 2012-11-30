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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClubStarterKit.Infrastructure.Application;
using ClubStarterKit.Infrastructure.Cache;
using StructureMap;

namespace ClubStarterKit.Infrastructure.Content.PageJavascript
{
    public class PageJavascriptContentAggrigator : HttpCacheBase<IEnumerable<PageJavascriptFile>>, IContentAggregator
    {
        /// <summary>
        /// Aggrigator of the JavaScript content in a website's Content/PageJavascript/ directory
        /// </summary>
        /// <param name="context"></param>
        /// <param name="applicationId"></param>
        public PageJavascriptContentAggrigator(HttpContextBase context, IApplicationIdProvider applicationId, string page)
            : base(context, applicationId)
        {
            Page = page;
        }

        /// <summary>
        /// Aggrigator of the JavaScript content in a website's Content/PageJavascript/ directory
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cache"></param>
        /// <param name="applicationId"></param>
        public PageJavascriptContentAggrigator(HttpContextBase context, CacheBase cache, IApplicationIdProvider applicationId, string page)
            : base(context, cache, applicationId)
        {
            Page = page;
        }

        public string Page { get; set; }        

        public override string ContentType
        {
            get { return "PageJavascript"; }
        }

        protected override IEnumerable<PageJavascriptFile> Grab()
        {
            var provider = ObjectFactory.GetInstance<ISiteConfigProvider>();
            return SiteContentUtils.GetFiles(ContentType, "js").Select(pageJs => new PageJavascriptFile(pageJs, provider));
        }

        public ActionResult ContentResult
        {
            get
            {
                var javascriptFile = CachedValue.Where(x => x.Filename.ToLower() == Page.ToLower()).FirstOrDefault();

                if (javascriptFile == null)
                    throw new Exception(string.Format("Page javascript file {0} was not found", Page));

                return new ContentResult
                {
                    Content = javascriptFile.Javascript,
                    ContentType = ContentConstants.JavascriptContentType
                };
            }
        }
    }
}
