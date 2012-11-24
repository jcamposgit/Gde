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

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web.Mvc;
using ClubStarterKit.Infrastructure.ActionResults;
using Moq;
using Xunit;

namespace ClubStarterKit.Tests.Infrastructure.ActionResults
{
    public class ImageResultTests
    {
        public ImageResultTests()
        {
            // setup control image
            var btmp = new Bitmap(50, 50);
            Graphics gr = Graphics.FromImage(btmp);
            gr.DrawRectangle(new Pen(Color.Black), 0, 0, 50, 50);
            GraphicsState save = gr.Save();
            ControlImage = btmp;
        }

        private Bitmap ControlImage { get; set; }

        [Fact]
        public void ImageResult_Constructor_WithTwoArguments_SetsImageProperty()
        {
            var result = new ImageResult(ControlImage, ImageFormat.Png);

            Assert.Equal(ControlImage, result.Image);
        }

        [Fact]
        public void ImageResult_Constructor_WithTwoArguments_SetsFormatProperty()
        {
            var result = new ImageResult(ControlImage, ImageFormat.Png);

            Assert.Equal(ImageFormat.Png, result.Format);
        }

        [Fact]
        public void ImageResult_Constructor_WithOneArgument_SetsImageProperty()
        {
            var result = new ImageResult(ControlImage);

            Assert.Equal(ControlImage, result.Image);
        }

        [Fact]
        public void ImageResult_Constructor_WithOneArguments_SetsFormatProperty()
        {
            var result = new ImageResult(ControlImage);

            Assert.NotNull(result.Format);
        }

        [Fact]
        public void ImageResult_ExecuteResult_SetsProperyContentType_WhenTheImageFormatIsBmp()
        {
            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.SetupSet(c => c.HttpContext.Response.ContentType = "image/bmp").Verifiable();

            new ImageResult(ControlImage, ImageFormat.Bmp).ExecuteResult(mockControllerContext.Object);

            mockControllerContext.Verify();
        }

        [Fact]
        public void ImageResult_ExecuteResult_SetsProperyContentType_WhenTheImageFormatIsPng()
        {
            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.SetupSet(c => c.HttpContext.Response.ContentType = "image/png").Verifiable();

            new ImageResult(ControlImage, ImageFormat.Png).ExecuteResult(mockControllerContext.Object);

            mockControllerContext.Verify();
        }
    }
}