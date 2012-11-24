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
using System.IO;

namespace ClubStarterKit.Infrastructure.Content.Images
{
    public class WebsiteImage
    {
        public WebsiteImage(FileInfo fileinfo)
        {
            // TODO: close file handle!
            Img = new Bitmap(new FileStream(fileinfo.FullName, FileMode.Open, FileAccess.Read));
            File = fileinfo;
        }

        /// <summary>
        /// Y-Value of the WebsiteImage in the CSS Sprite
        /// </summary>
        public int YValue { get; internal set; }

        /// <summary>
        /// Bitmap representation of the image
        /// </summary>
        public Bitmap Img { get; protected set; }

        /// <summary>
        /// File location (in content directory) of the WebsiteImage
        /// </summary>
        public FileInfo File { get; protected set; }

        /// <summary>
        /// Image width
        /// </summary>
        public int Width
        {
            get { return Img.Width; }
        }

        /// <summary>
        /// Image height
        /// </summary>
        public int Height
        {
            get { return Img.Height; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>File name in lower case</returns>
        public override string ToString()
        {
            return Path.GetFileNameWithoutExtension(File.Name).ToLower();
        }
    }
}