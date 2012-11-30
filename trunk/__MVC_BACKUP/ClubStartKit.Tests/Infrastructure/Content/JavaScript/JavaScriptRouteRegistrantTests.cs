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
using ClubStarterKit.Infrastructure.Content.Javascript;
using ClubStarterKit.Tests.Helpers;
using Xunit;

namespace ClubStarterKit.Tests.Infrastructure.Content.JavaScript
{
    public class JavaScriptRouteRegistrantTests
    {
        [Fact]
        public void JavaScriptRouteRegistrant_Register_SiteImageRoute_MapsToContentController()
        {
            var data = RouteTester.Test(new JavaScriptRouteRegistrant(), "~/sitecontent/javascript/myappid");
            Assert.Equal("Content", data.Values[RouteTester.ControllerValue].ToString());
        }

        [Fact]
        public void JavaScriptRouteRegistrant_Register_SiteImageRoute_MapsToJavaScriptAction()
        {
            var data = RouteTester.Test(new JavaScriptRouteRegistrant(), "~/sitecontent/javascript/myappid");
            Assert.Equal("JavaScript", data.Values[RouteTester.ActionValue].ToString());
        }

        [Fact]
        public void JavaScriptRouteRegistrant_Register_SiteImageRoute_SetsAppIdValueProperly()
        {
            var data = RouteTester.Test(new JavaScriptRouteRegistrant(), "~/sitecontent/javascript/myappid");
            Assert.Equal("myappid", data.Values[ContentConstants.ApplicationIdParameterName].ToString());
        }
    }
}
