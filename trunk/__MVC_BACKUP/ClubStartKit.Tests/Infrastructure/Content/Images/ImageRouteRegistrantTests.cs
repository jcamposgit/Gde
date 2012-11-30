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
using ClubStarterKit.Infrastructure.Content.Images;
using ClubStarterKit.Tests.Helpers;
using Xunit;

namespace ClubStarterKit.Tests.Infrastructure.Content.Images
{
    public class ImageRouteRegistrantTests
    {
        [Fact]
        public void ImageRouteRegistrant_Register_SiteImageRoute_MapsToContentController()
        {
            var data = RouteTester.Test(new ImageRouteRegistrant(), "~/sitecontent/image/myfile/myappid");
            Assert.Equal("Content", data.Values[RouteTester.ControllerValue].ToString());
        }

        [Fact]
        public void ImageRouteRegistrant_Register_SiteImageRoute_MapsToImageAction()
        {
            var data = RouteTester.Test(new ImageRouteRegistrant(), "~/sitecontent/image/myfile/myappid");
            Assert.Equal("Image", data.Values[RouteTester.ActionValue].ToString());
        }

        [Fact]
        public void ImageRouteRegistrant_Register_SiteImageRoute_SetsFileValueProperly()
        {
            var data = RouteTester.Test(new ImageRouteRegistrant(), "~/sitecontent/image/myfile/myappid");
            Assert.Equal("myfile", data.Values[ContentConstants.FileParameterName].ToString());
        }

        [Fact]
        public void ImageRouteRegistrant_Register_SiteImageRoute_SetsAppIdValueProperly()
        {
            var data = RouteTester.Test(new ImageRouteRegistrant(), "~/sitecontent/image/myfile/myappid");
            Assert.Equal("myappid", data.Values[ContentConstants.ApplicationIdParameterName].ToString());
        }

        [Fact]
        public void ImageRouteRegistrant_Register_SpriteRoute_MapsToContentController()
        {
            var data = RouteTester.Test(new ImageRouteRegistrant(), "~/sitecontent/sprite/myappid");
            Assert.Equal("Content", data.Values[RouteTester.ControllerValue].ToString());
        }

        [Fact]
        public void ImageRouteRegistrant_Register_SpriteRoute_MapsToImageAction()
        {
            var data = RouteTester.Test(new ImageRouteRegistrant(), "~/sitecontent/sprite/myappid");
            Assert.Equal("Sprite", data.Values[RouteTester.ActionValue].ToString());
        }

        [Fact]
        public void ImageRouteRegistrant_Register_SpriteRoute_SetsFileValueProperly()
        {
            var data = RouteTester.Test(new ImageRouteRegistrant(), "~/sitecontent/sprite/myappid");
            Assert.Equal("myappid", data.Values[ContentConstants.ApplicationIdParameterName].ToString());
        }
    }
}
