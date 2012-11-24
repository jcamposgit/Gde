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
using System.Drawing;
using System.IO;
using ClubStarterKit.Infrastructure.Content.Images;
using Xunit;

namespace ClubStarterKit.Tests.Infrastructure.Content.Images
{
    public class WebsiteImageCollectionTests
    {
        private const string Image2FileName = "webimage2";
        private const string ImageFileName = "webimage1";
        private static readonly string fileLocation = Environment.CurrentDirectory + @"\{0}.bmp";

        public WebsiteImageCollectionTests()
        {
            // setup ControlImage
            Image1 = SetupImage(new FileInfo(string.Format(fileLocation, ImageFileName)), new Bitmap(50, 50),
                                Color.Black);

            // setup ControlImage2
            Image2 = SetupImage(new FileInfo(string.Format(fileLocation, Image2FileName)), new Bitmap(100, 150),
                                Color.Red);
        }

        public WebsiteImage Image1 { get; set; }
        public WebsiteImage Image2 { get; set; }

        private WebsiteImage SetupImage(FileInfo file, Bitmap bmp, Color color)
        {
            // setup control image
            if (!file.Exists)
            {
                // draw a rectangle
                using (Graphics gr = Graphics.FromImage(bmp))
                    gr.DrawRectangle(new Pen(color), 0, 0, bmp.Width, bmp.Height);


                // save the image off to a temporary location
                bmp.Save(file.FullName);
                return new WebsiteImage(file);
            }
            else
                return new WebsiteImage(file);
        }

        [Fact]
        public void WebsiteImageCollection_TotalHeight_EqualsSumOfHeightsOnTwoImages()
        {
            var wic = new WebsiteImageCollection(new[] {Image1, Image2});
            int totalHeightExpected = Image1.Height + Image2.Height;

            Assert.Equal(totalHeightExpected, wic.TotalHeight);
        }

        [Fact]
        public void WebsiteImageCollection_TotalHeight_EqualsZeroWithZeroItems()
        {
            var wic = new WebsiteImageCollection(new WebsiteImage[0]);
            int totalHeightExpected = 0;

            Assert.Equal(totalHeightExpected, wic.TotalHeight);
        }

        [Fact]
        public void WebsiteImageCollection_MaxWidth_EqualsHighestWidth()
        {
            var wic = new WebsiteImageCollection(new[] {Image1, Image2});
            int widthExpected = Image2.Width;

            Assert.Equal(widthExpected, wic.MaxWidth);
        }

        [Fact]
        public void WebsiteImageCollection_MaxWidth_EqualsZeroWithZeroItems()
        {
            var wic = new WebsiteImageCollection(new WebsiteImage[0]);
            int widthExpected = 0;

            Assert.Equal(widthExpected, wic.MaxWidth);
        }

        [Fact]
        public void WebsiteImageCollection_Sprite_WidthEqualsHighestWidth()
        {
            var wic = new WebsiteImageCollection(new[] {Image1, Image2});
            int widthExpected = wic.MaxWidth;

            Assert.Equal(widthExpected, wic.Sprite.Width);
        }

        [Fact]
        public void WebsiteImageCollection_Sprite_HeightEqualsTotalWidth()
        {
            var wic = new WebsiteImageCollection(new[] {Image1, Image2});
            int totalHeight = wic.TotalHeight;

            Assert.Equal(totalHeight, wic.Sprite.Height);
        }
    }
}