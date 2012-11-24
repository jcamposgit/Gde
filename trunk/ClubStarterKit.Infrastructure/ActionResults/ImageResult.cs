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
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;

namespace ClubStarterKit.Infrastructure.ActionResults
{
    /// <summary>
    /// Binary bitmap result
    /// </summary>
    public class ImageResult : ActionResult
    {
        /// <summary>
        /// Binds the ImageResult to an image
        /// </summary>
        /// <param name="image">Bitmap image to write to output</param>
        /// <param name="format">Image format (default: PNG)</param>
        public ImageResult(Bitmap image, ImageFormat format = null)
        {
            Image = image;
            Format = format ?? ImageFormat.Png;
        }

        /// <summary>
        /// Resuting image format written to output
        /// </summary>
        public ImageFormat Format { get; set; }

        /// <summary>
        /// Image to be written to output
        /// </summary>
        public Bitmap Image { get; set; }

        /// <summary>
        /// Set the far future expire on the <see cref="HttpResponse"/>
        /// </summary>
        public bool SetFarFutureExpire { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (Image == null)
                throw new NullReferenceException("Image cannot be null");

            if (context == null)
                throw new NullReferenceException("Controller context cannot be null");

            var response = context.HttpContext.Response;
            using (var stream = new MemoryStream())
            {
                Image.Save(stream, Format);
                response.BinaryWrite(stream.ToArray());
            }

            response.ContentType = "image/" + (Format ?? ImageFormat.Png).ToString().ToLower();
        }
    }
}