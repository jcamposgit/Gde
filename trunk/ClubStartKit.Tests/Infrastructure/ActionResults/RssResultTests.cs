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

using System.IO;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using ClubStarterKit.Infrastructure.ActionResults;
using Moq;
using Xunit;

namespace ClubStarterKit.Tests.Infrastructure.ActionResults
{
    public class RssResultTests
    {
        [Fact]
        public void RssResult_ExecuteResult_SetsOutputTypeToRss()
        {
            var httpContext = new Mock<HttpContextBase>();
            var httpResponse = new Mock<HttpResponseBase>();
            httpContext.Setup(x => x.Response).Returns(httpResponse.Object);
            httpResponse.Setup(x => x.Output).Returns(new StringWriter());
            httpResponse.SetupSet(x => x.ContentType = "application/rss+xml").Verifiable();
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(x => x.HttpContext).Returns(httpContext.Object);

            new RssResult(new SyndicationFeed()).ExecuteResult(controllerContext.Object);

            httpContext.VerifyAll();
        }

        [Fact]
        public void RssResult_ExecuteResult_WritesValueToTheOutput()
        {
            var writer = new StringWriter();
            var httpContext = new Mock<HttpContextBase>();
            var httpResponse = new Mock<HttpResponseBase>();
            httpContext.Setup(x => x.Response).Returns(httpResponse.Object);
            httpResponse.Setup(x => x.Output).Returns(writer);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(x => x.HttpContext).Returns(httpContext.Object);

            new RssResult(new SyndicationFeed()).ExecuteResult(controllerContext.Object);

            Assert.NotEmpty(writer.ToString());
        }
    }
}