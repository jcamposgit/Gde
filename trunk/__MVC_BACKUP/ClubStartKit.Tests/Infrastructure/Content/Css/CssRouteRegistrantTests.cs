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
using ClubStarterKit.Infrastructure.Content.Css;
using ClubStarterKit.Tests.Helpers;
using Xunit;

namespace ClubStarterKit.Tests.Infrastructure.Content.Css
{
    public class CssRouteRegistrantTests
    {
        [Fact]
        public void CssRouteRegistrant_Register_RoutesContentController()
        {
            RouteData data = RouteTester.Test(new CssRouteRegistrant(),
                                              "~/sitecontent/css");

            Assert.Equal("Content", data.Values[RouteTester.ControllerValue]);
        }

        [Fact]
        public void CssRouteRegistrant_Register_RoutesCssAction()
        {
            RouteData data = RouteTester.Test(new CssRouteRegistrant(),
                                              "~/sitecontent/css");

            Assert.Equal("Css", data.Values[RouteTester.ActionValue]);
        }
    }
}