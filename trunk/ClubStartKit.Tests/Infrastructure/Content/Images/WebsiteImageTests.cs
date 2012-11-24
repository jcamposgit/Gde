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
    public class WebsiteImageTests
    {
        private string fileLocation = Environment.CurrentDirectory + @"\" + fileName + ".bmp";
        private const string fileName = "tEst";

        public WebsiteImageTests()
        {
            ControlImageLocation = new FileInfo(fileLocation);

            // setup control image
            if (!ControlImageLocation.Exists)
            {
                var btmp = new Bitmap(50, 50);
                // draw a rectangle
                using (Graphics gr = Graphics.FromImage(btmp))
                    gr.DrawRectangle(new Pen(Color.Black), 0, 0, 50, 50);

                // save the image off to a temporary location
                btmp.Save(fileLocation);
                ControlImage = btmp;
            }
            else
                ControlImage = new Bitmap(fileLocation);
        }

        private Bitmap ControlImage { get; set; }
        private FileInfo ControlImageLocation { get; set; }

        [Fact]
        public void WebsiteImage_Contructor_SetsFileProperty()
        {
            var websiteImage = new WebsiteImage(ControlImageLocation);
            Assert.Same(ControlImageLocation, websiteImage.File);
        }

        [Fact]
        public void WebsiteImage_Contrsuctor_SetsImgProperty()
        {
            // WARNING: This test assumes the control and the testing image are equal by dimension
            var websiteImage = new WebsiteImage(ControlImageLocation);
            Assert.Equal(ControlImage.Size, websiteImage.Img.Size);
        }

        [Fact]
        public void WebsiteImage_ToString_ShowsTheNameOfTheFileInLowerCaseWithoutExt()
        {
            var websiteImage = new WebsiteImage(ControlImageLocation);
            Assert.Equal(fileName.ToLower(), websiteImage.ToString());
        }
    }
}