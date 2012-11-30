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

using System.Linq;
using ClubStarterKit.Tests.Helpers;
using ClubStarterKit.Web.Helper.Routing;
using Xunit;
using Xunit.Extensions;

namespace ClubStarterKit.Tests.Web.Helpers.Routing
{
    public class ForumRouteRegistrantTests
    {
        [Fact]
        public void ForumRouteRegistrant_Register_ThreadView_MapsToThreadController()
        {
            var data = RouteTester.Test(new ForumRouteRegistrant(), "~/forum/thread/foo");

            Assert.Equal("Thread", data.Values[RouteTester.ControllerValue]);
        }

        [Fact]
        public void ForumRouteRegistrant_Register_ThreadView_MapsToViewAction()
        {
            var data = RouteTester.Test(new ForumRouteRegistrant(), "~/forum/thread/foo");

            Assert.Equal("View", data.Values[RouteTester.ActionValue]);
        }

        [Fact]
        public void ForumRouteRegistrant_Register_ThreadView_HasIDValue()
        {
            var id = "foo";
            var data = RouteTester.Test(new ForumRouteRegistrant(), "~/forum/thread/" + id);

            Assert.Equal(id, data.Values["id"]);
        }

        [Fact]
        [AssumeIdentity("identity")]
        public void ForumRouteRegistrant_Register_TheadAction_MapsToThreadController()
        {
            var data = RouteTester.Test(new ForumRouteRegistrant(), "~/forum/thread/foo/new");

            Assert.Equal("Thread", data.Values[RouteTester.ControllerValue]);
        }

        [Fact]
        public void ForumRouteRegistrant_Register_IgnoreThread_IgnoresThreadRoot()
        {
            var data = RouteTester.Test(new ForumRouteRegistrant(), "~/thread/view/1");

            // make sure it doesn't map to thread controller
            Assert.False(data.Values.Keys.Contains(RouteTester.ControllerValue));
        }
    }
}
