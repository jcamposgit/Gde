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
using ClubStarterKit.Tests.Helpers;
using ClubStarterKit.Web.Helpers.Routing;
using Xunit;

namespace ClubStarterKit.Tests.Web.Helpers.Routing
{
    public class PageRouteRegistrantTests
    {
        [Fact]
        public void PageRouteRegistrant_Register_RegistersPageController()
        {
            RouteData data = RouteTester.Test(new PageRouteRegistrant(), "~/page/sample");

            Assert.Equal("ContentPage", data.Values[RouteTester.ControllerValue]);
        }

        [Fact]
        public void PageRouteRegistrant_Register_RegistersPageAction()
        {
            RouteData data = RouteTester.Test(new PageRouteRegistrant(), "~/page/sample");

            Assert.Equal("Page", data.Values[RouteTester.ActionValue]);
        }

        [Fact]
        public void PageRouteRegistrant_Register_RegistersIdValueFromUrl()
        {
            string idValue = "sample";
            RouteData data = RouteTester.Test(new PageRouteRegistrant(), string.Format("~/page/{0}", idValue));

            Assert.Equal(idValue, data.Values["id"]);
        }
    }
}