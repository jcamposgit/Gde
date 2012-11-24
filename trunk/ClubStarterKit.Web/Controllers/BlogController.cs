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
using System.Web.Mvc;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure;
using ClubStarterKit.Infrastructure.ActionResults;
using ClubStarterKit.Web.Infrastructure.Blogs;
using ClubStarterKit.Web.Infrastructure.Membership;
using ClubStarterKit.Web.Infrastructure.Membership.Identification;
using ClubStarterKit.Web.ViewData.Blogs;

namespace ClubStarterKit.Web.Controllers
{
    public partial class BlogController : BaseController
    {
        public virtual ActionResult Index()
        {
            ViewData.Title("Blog Posts");
            SetRss();

            // setup header, pager action, and rss action
            new ListViewData(ViewData, "");
            return View(Views.List, new BlogPostListAction(0).Execute());
        }

        private void SetRss()
        {
            this.Rss(Website.Blog.Rss(), "Blogs");
        }

        public virtual ActionResult List(int id = 1)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException("page", id, "Value of page cannot be less than 1");

            ViewData.Title("Blog Posts (Page " + id + ")");
            SetRss();
            var list = new BlogPostListAction(id - 1).Execute();

            // setup header, pager action, and rss action
            new ListViewData(ViewData, "");
            return AjaxTemplatedResult(Views.DisplayTemplates.PagedBlogPosts, Views.List, list);
        }

        public virtual ActionResult Author(string id, int? page)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            ViewData.Title("Blog Posts by " + id);

            // setup header, pager action, and rss action
            new ListViewData(ViewData, id);

            return AjaxTemplatedResult
            (
                Views.DisplayTemplates.PagedBlogPosts, 
                Views.List, 
                new AuthorBlogPostListAction((page ?? 1) - 1, id).Execute()
            );
        }

        public virtual ActionResult Rss(string id = "")
        {
            return new RssResult(new BlogRssAction(Url, id).Execute());
        }

        [ActionName("View")]
        public virtual ActionResult Show(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction(Website.Blog.Index());

            var post = new BlogPostRetrievalAction(id).Execute();

            if(post == null)
                throw new Exception(string.Format("The post with id {0} was not found.", id));

            ViewData.Title("Blog Post - " + post.Post.Title);
            return View(Views.View, post);
        }

        [Admin]
        public virtual ActionResult Edit(string id)
        {
            var post = new BlogPostRetrievalAction(id).Execute().Post;
            ViewData.Title("Blog Post Edit - " + post.Title);
            return View(Views.Edit, post);
        }

        [Admin]
        public virtual ActionResult New()
        {
            ViewData.Title("New Blog Post");
            return View(Views.Edit, new BlogPost());
        }

        [Admin, ValidateInput(false)]
        public virtual ActionResult Update(BlogPost post)
        {
            if (post == null)
                throw new ArgumentNullException("post");

            post.Author = UserRetrieval.GetUser(HttpContext).Return();

            if (new BlogUpdateAction(post).Execute())
                return RedirectToAction(Website.Blog.Show(post.Slug));

            return View(Views.Edit, post);
        }

        [Admin]
        public virtual ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            new BlogPostDeleteAction(id).Execute();

            return RedirectToAction(Website.Blog.Index());
        }

        #region Commenting

        [Authorize, ValidateInput(false)]
        public virtual ActionResult AddComment(string commentText, BlogPost post)
        {
            if (string.IsNullOrEmpty(commentText))
                throw new ArgumentNullException("commentText");

            var user = UserRetrieval.GetUser(HttpContext);

            if (user.IsNothing)
                throw new Exception("user is invalid.");

            var result = new CommentAddAction(new BlogComment()
            {
                CommentDate = DateTimeOffset.Now,
                CommentText = commentText.RemoveHtml(),
                Commenter = user.Return(),
                Post = post
            }).Execute();

            return new AjaxDeterministicResult
            (
                () => View(Views.DisplayTemplates.WithPath.CommentsView, new CommentsRetrievalAction(post.Id).Execute()),
                () => RedirectToAction(Website.Blog.Show(post.Slug))
            );
        }

        [Admin]
        public virtual ActionResult DeleteComment(BlogComment comment)
        {
            if(comment == null)
                throw new ArgumentNullException("comment");

            var result = new CommentDeleteAction(comment).Execute();

            return new AjaxDeterministicResult
            (
                () => View(Views.DisplayTemplates.WithPath.CommentsView, new CommentsRetrievalAction(comment.Post.Id).Execute()),
                () => RedirectToAction(Website.Blog.Show(comment.Post.Slug))
            );
        }

        #endregion
    }
}