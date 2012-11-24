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

using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ClubStarterKit.Infrastructure.Routing
{
    public class WwwHandler : MvcHandler
    {
        public WwwHandler(RequestContext requestContext) : base(requestContext)
        {
        }

        protected override void ProcessRequest(HttpContextBase httpContext)
        {
            //We do not want this handler to process local requests
            if (httpContext.Request.IsLocal)
                base.ProcessRequest(httpContext);
            else
                ProcessExternalRequest(httpContext);
        }

        private void ProcessExternalRequest(HttpContextBase httpContext)
        {
            bool urlChanged = false;
            string url = RequestContext.HttpContext.Request.Url.AbsoluteUri;
            //Check for non-www version URL
            if (!RequestContext.HttpContext.Request.Url.AbsoluteUri.Contains("www"))
            {
                urlChanged = true;
                //change to www. version URL
                url = url.Replace("http://", "http://www.");
            }
            ProcessExternalRequest(url, urlChanged, httpContext);
        }

        private void ProcessExternalRequest(string url, bool urlChanged, HttpContextBase httpContext)
        {
            if (urlChanged)
            {
                //mark as 301
                httpContext.Response.Status = "301 Moved Permanently";
                httpContext.Response.StatusCode = 301;
                httpContext.Response.AppendHeader("Location", url);
            }
            else
                base.ProcessRequest(httpContext);
        }
    }
}