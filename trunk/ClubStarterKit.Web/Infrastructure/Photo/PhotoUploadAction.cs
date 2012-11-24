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
using System.Linq;
using System.Web;
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure.Cache;
using ClubStarterKit.Web.Infrastructure.Membership.Identification;

namespace ClubStarterKit.Web.Infrastructure.Photo
{
    public class PhotoUploadAction : IDataAction<PhotoUploadAction.PhotoUploadContext, bool>
    {
        public PhotoUploadAction(HttpContextBase httpContext, string album, string title)
        {
            var owner = UserRetrieval.GetUser(httpContext);

            if(owner.IsNothing)
                throw new Exception("User credentials invalid");

            Context = new PhotoUploadContext
            {
                HttpContext = httpContext,
                Album = album,
                Title = title,
                Owner = owner.Return()
            };
        }

        public class PhotoUploadContext
        {
            public HttpContextBase HttpContext { get; set; }
            public string Album { get; set; }
            public string Title { get; set; }
            public User Owner { get; set; }
        }

        public PhotoUploadContext Context { get; private set; }

        public bool Execute()
        {
            try
            {
                using(var scope = new UnitOfWorkScope())
                {
                    var files = Context.HttpContext.Request.Files;
                    var album = scope.UnitOfWork.RepositoryFor<Album>().First(a=>a.Slug == Context.Album);
                    var fileUrlBase = Context.HttpContext.Server.MapPath(Constants.PhotoUploadFolder);
                    var repository = scope.UnitOfWork.RepositoryFor<Domain.Photo>();

                    for(int i = 0; i < files.Count; i++)
                    {
                        // upload file
                        var file = Context.HttpContext.Request.Files.Get(0);
                        var loc =  Guid.NewGuid().ToString().Replace("-", "").ToLower() + file.FileName.ToLower();
                        Upload(file.InputStream, fileUrlBase + loc);

                        // save a photo to the DB
                        repository.Save(new Domain.Photo
                        {
                            Album = album,
                            Title = Context.Title,
                            Position = 0, // TODO: Add position change ability
                            Owner = Context.Owner,
                            PhotoLocation = loc
                        });
                    }

                    scope.Commit();
                }

                CacheKeyStore.ExpireWithType<Domain.Photo>(Context.Album);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void Upload(Stream stream, string location)
        {
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);
            byte[] img = Crop(data, Constants.ImageMaxWidth);
            var fileStream = new FileStream(location, FileMode.Create);
            fileStream.Write(img, 0, img.Length);
            fileStream.Close();
        }

        private static byte[] Crop(byte[] image, int maxWidth)
        {
            var orig = Image.FromStream(new MemoryStream(image));

            if (orig.Width > maxWidth)
            {
                double scale = orig.Width / maxWidth;
                int height = (int)(orig.Height / scale);
                var img = new Bitmap(orig, maxWidth, height);
                var stream = new MemoryStream();
                img.Save(stream, ImageFormat.Jpeg);
                return stream.GetBuffer();
            }
            else
                return image;
        }
    }
}