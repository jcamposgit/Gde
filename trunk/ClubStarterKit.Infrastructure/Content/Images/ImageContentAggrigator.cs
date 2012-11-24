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
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClubStarterKit.Infrastructure.ActionResults;
using ClubStarterKit.Infrastructure.Application;
using ClubStarterKit.Infrastructure.Cache;

namespace ClubStarterKit.Infrastructure.Content.Images
{
    public class ImageContentAggrigator : HttpCacheBase<WebsiteImageCollection>, IContentAggregator
    {
        public ImageContentAggrigator(HttpContextBase context, IApplicationIdProvider applicationId, string fileName)
            : base(context, applicationId)
        {
            FileName = fileName;
        }

        public ImageContentAggrigator(HttpContextBase context, IApplicationIdProvider applicationId)
            : this(context, applicationId, string.Empty)
        {
        }

        public ImageContentAggrigator(HttpContextBase context, CacheBase cache, IApplicationIdProvider applicationId)
            : base(context, cache, applicationId)
        {
            FileName = string.Empty;
        }

        public string FileName { get; set; }

        protected override WebsiteImageCollection Grab()
        {
            var files = from f in SiteContentUtils.GetFiles(ContentType, "*")
                        select new WebsiteImage(f);
            return new WebsiteImageCollection(files);
        }

        #region IContentAggregator Members

        public override string ContentType
        {
            get { return "Images"; }
        }

        public ActionResult ContentResult
        {
            get
            {
                // not looking for a specific image
                if (string.IsNullOrEmpty(FileName))
                    return SpriteResult();

                // look for the specified image
                ImageFormat format = ImageFormat.Png;
                var foundImage = CachedValue.Where(image => image.ToString().Equals(FileName, StringComparison.OrdinalIgnoreCase))
                                       .FirstOrDefault();

                // image not found, just return the sprite
                if (foundImage == null)
                    return SpriteResult();

                // preserve the file format by finding the ImageFormat from the extension
                switch (foundImage.File.Extension.ToLower().Replace(".", ""))
                {
                    case "gif":
                        format = ImageFormat.Gif;
                        break;
                    case "jpeg":
                        format = ImageFormat.Jpeg;
                        break;
                    case "jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case "png":
                        format = ImageFormat.Png;
                        break;
                    case "bmp":
                        format = ImageFormat.Bmp;
                        break;
                    default:
                        break;
                }

                return new ImageResult(foundImage.Img, format);
            }
        }

        private ImageResult SpriteResult()
        {
            return new ImageResult(CachedValue.Sprite, ImageFormat.Png);
        }

        #endregion
    }
}