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

using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ClubStarterKit.Infrastructure.Content.Images
{
    public class WebsiteImageCollection : List<WebsiteImage>
    {
        private Bitmap _sprite;

        public WebsiteImageCollection(IEnumerable<WebsiteImage> images)
            : base(images)
        {
            // auto-load sprite to setup the Y-values
            if (images.Count() > 0)
            {
                Bitmap b = Sprite;
            }
        }

        public int MaxWidth
        {
            get
            {
                return Count == 0 ? 0 : this.Max(x => x.Width);
            }
        }

        public int TotalHeight
        {
            get
            {
                return this.Sum(x => x.Height);
            }
        }

        public Bitmap Sprite
        {
            get
            {
                if (_sprite == null)
                {
                    var spriteImg = new Bitmap(MaxWidth, TotalHeight);
                    Graphics sprite = Graphics.FromImage(spriteImg);

                    int currentYvalue = 0;

                    ForEach(image =>
                                {
                                    // draw image onto sprite
                                    sprite.DrawImage(image.Img, 0, currentYvalue);
                                    // set the Y-Value on the image to the Y-Value in the sprite
                                    image.YValue = currentYvalue;
                                    // add the current height to the Y-Value for the NEXT image
                                    currentYvalue += image.Height;
                                });

                    _sprite = spriteImg;
                }

                return _sprite;
            }
        }
    }
}