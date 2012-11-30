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
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ClubStarterKit.Infrastructure.Application;
using ClubStarterKit.Infrastructure.Cache;
using StructureMap;

namespace ClubStarterKit.Infrastructure.Content.Javascript
{
    /// <summary>
    /// Aggrigator of the JavaScript content in a website's Content/JavaScript/ directory
    /// </summary>
    public class JavaScriptContentAggrigator : HttpCacheBase<string>, IContentAggregator
    {
        /// <summary>
        /// Aggrigator of the JavaScript content in a website's Content/JavaScript/ directory
        /// </summary>
        /// <param name="context"></param>
        /// <param name="applicationId"></param>
        public JavaScriptContentAggrigator(HttpContextBase context, IApplicationIdProvider applicationId)
            : base(context, applicationId)
        {
        }

        /// <summary>
        /// Aggrigator of the JavaScript content in a website's Content/JavaScript/ directory
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cache"></param>
        /// <param name="applicationId"></param>
        public JavaScriptContentAggrigator(HttpContextBase context, CacheBase cache,
                                           IApplicationIdProvider applicationId)
            : base(context, cache, applicationId)
        {
        }

        #region IContentAggregator Members

        public override string ContentType
        {
            get { return "Javascript"; }
        }

        public ActionResult ContentResult
        {
            get
            {
                return new ContentResult
                           {
                               Content = CachedValue,
                               ContentType = ContentConstants.JavascriptContentType
                           };
            }
        }

        #endregion

        protected override string Grab()
        {
            if (ObjectFactory.GetInstance<ISiteConfigProvider>().MinifyJavascript)
                return new JavaScriptMinifier().Minify(Content + AdditionalItems);
            else
                return Content + AdditionalItems;
        }

        #region Content Grabbers

        protected virtual string Content
        {
            get 
            {
                var files = SiteContentUtils.GetFiles(ContentType, "js").ToStringArray();
                return new JavaScriptSorter(files).ToString();
            }
        }

        protected virtual string AdditionalItems
        {
            get
            {
                var builder = new StringBuilder();
                new BinPartLoader<IJavaScriptAddition>().Items.Select(x => x.Addition).Foreach(x => builder.Append(x));
                return builder.ToString();
            }
        }

        #endregion
    }
}