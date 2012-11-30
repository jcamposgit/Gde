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
using System.Linq;
using System.Web.Mvc;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure;
using ClubStarterKit.Web.Infrastructure.Membership;
using ClubStarterKit.Web.Infrastructure.Membership.Identification;
using ClubStarterKit.Web.Infrastructure.Photo;

namespace ClubStarterKit.Web.Controllers
{
    public partial class PhotosController : BaseController
    {
        public virtual ActionResult Index()
        {
            var albums = new AlbumListAction().Execute();
            
            // filter when not authenticated
            if (!User.Identity.IsAuthenticated)
                albums = albums.Where(x => x.AllowAnonymous);

            ViewData.Title("Photo Albums");
            return View(Views.Albums, albums);
        }

        [ActionName("View")]
        public virtual ActionResult ViewAlbum(string id /*ALBUM*/, int photo /*index*/)
        {
            var list = new PhotoListAction(id, photo - 1).Execute();

            ViewData["Album"] = list.Count == 0 ? new AlbumRetrievalAction(id).Execute() : list[0].Album;

            ViewData.Title("Photos - " + (ViewData["Album"] as Album).Title);

            return AjaxTemplatedResult(Views.DisplayTemplates.Photo, Views.View, list);
        }

        public virtual ActionResult Photo(string id, string app)
        { 
            var output = "image/";
            if(id.EndsWith("jpg"))
                output += "jpg";
            else 
                output += "png";
            return new FilePathResult(Server.MapPath(Constants.PhotoUploadFolder + id), output);
        }

        [Admin]
        public virtual ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("album");
            var user = UserRetrieval.GetUser(HttpContext);
            if (user.IsNothing)
                throw new Exception("Invalid user principal");
            var album = new AlbumRetrievalAction(id).Execute();
            album.Owner = user.Return();
            ViewData.Title("Edit Album - " + album.Title);
            return View(Views.Edit, album);
        }

        [Admin]
        public virtual ActionResult New()
        {
            var user = UserRetrieval.GetUser(HttpContext);
            if (user.IsNothing)
                throw new Exception("Invalid user principal");
            ViewData.Title("New Album");
            return View(Views.Edit, new Album
            {
                Owner = user.Return(),
                AllowAnonymous = true,
                DateCreated = DateTimeOffset.UtcNow
            });
        }

        [Admin, HttpPost]
        public virtual ActionResult Update(Album album)
        {
            var result = new AlbumUpdateAction(album).Execute();
            if (!result)
                return View(Views.Edit, album);
            return RedirectToAction(Website.Photos.Index());
        }

        [Admin]
        public virtual ActionResult Delete(int id, int photo)
        {
            return Json(new { success = new PhotoDeleteAction(photo, id).Execute() });
        }

        [Admin, HttpPost]
        public virtual ActionResult Upload(string album, string title = "")
        {
            new PhotoUploadAction(HttpContext, album, title).Execute();
            return RedirectToAction(Website.Photos.ViewAlbum(album, 1));
        }

        public virtual ActionResult Comments(int id)
        {
            if (Constants.DisablePhotoCommentViewForUnauthorized && !User.Identity.IsAuthenticated)
                return new EmptyResult();
            
            return View(Views.Comments, new PhotoCommentListAction(id).Execute());
        }

        [Admin, HttpPost]
        public virtual ActionResult DeleteComment(PhotoComment comment)
        {
            new PhotoCommentDeleteAction(comment).Execute();
            return View(Views.Comments, new PhotoCommentListAction(comment.Photo.Id).Execute());
        }

        [Authorize, HttpPost]
        public virtual ActionResult AddComment(int photo, string comment)
        {
            var user = UserRetrieval.GetUser(HttpContext);

            if(user.IsNothing)
                throw new Exception("Invalid user principal");

            new PhotoCommentAddAction(photo, comment, user.Return()).Execute();
            return View(Views.Comments, new PhotoCommentListAction(photo).Execute());
        }
    }
}