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
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure.Cache;

namespace ClubStarterKit.Web.Infrastructure.Photo
{
    public class PhotoCommentAddAction : IDataAction<PhotoCommentAddAction.AddCommentContext, bool>
    {
        public class AddCommentContext
        {
            public User User { get; set; }
            public string Comment { get; set; }
            public int Photo { get; set; }
        }

        public PhotoCommentAddAction(int photo, string comment, User user)
        {
            Context = new AddCommentContext
            {
                Comment = comment,
                User = user,
                Photo = photo
            };
        }

        public PhotoCommentAddAction.AddCommentContext Context { get; private set; }

        public bool Execute()
        {
            try
            {
                using (var scope = new UnitOfWorkScope())
                {
                    var photo = scope.UnitOfWork.RepositoryFor<Domain.Photo>().First(p => p.Id == Context.Photo);
                    scope.UnitOfWork.RepositoryFor<PhotoComment>().Save(new PhotoComment
                    {
                        Commenter = Context.User,
                        Photo = photo,
                        CommentDate = DateTimeOffset.UtcNow,
                        CommentText = Context.Comment
                    });
                    scope.Commit();
                }
                CacheKeyStore.ExpireWithType<PhotoComment>(Context.Photo.ToString());
                return true;
            }
            catch
            {
                return false;   
            }
        }
    }
}