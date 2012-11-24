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
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;

namespace ClubStarterKit.Infrastructure.ActionResults
{
    /// <summary>
    /// RSS result for a <see cref="SyndicationFeed"/>
    /// </summary>
    public class RssResult : ActionResult
    {
        /// <summary>
        /// Binds the Rss Result to a <see cref="SyndicationFeed"/>
        /// </summary>
        /// <param name="feed"><see cref="SyndicationFeed"/> to use in the output</param>
        public RssResult(SyndicationFeed feed)
        {
            if (feed == null)
                throw new ArgumentNullException("feed");

            DataFeed = feed;
        }

        public SyndicationFeed DataFeed { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (DataFeed == null)
                throw new NullReferenceException("DataFeed cannot be null");

            if (context == null)
                throw new NullReferenceException("Controller context cannot be null");

            context.HttpContext.Response.ContentType = "application/rss+xml";

            using (var writer = XmlWriter.Create(context.HttpContext.Response.Output))
                new Rss20FeedFormatter(DataFeed).WriteTo(writer);
        }
    }
}