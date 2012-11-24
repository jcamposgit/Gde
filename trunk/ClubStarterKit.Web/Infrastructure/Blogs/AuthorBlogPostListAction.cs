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
using ClubStarterKit.Core;
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure.Cache;

namespace ClubStarterKit.Web.Infrastructure.Blogs
{
    public class AuthorBlogPostListAction : IDataAction<Tuple<string, int>, IPagedList<BlogPost>>
    {
        public AuthorBlogPostListAction(int index, string author)
        {
            Context = new Tuple<string, int>(author, index);
        }

        #region Implementation of IDataAction<Tuple<string, int>, IPagedList<BlogPost>>

        public Tuple<string, int> Context { get; private set; }
        public IPagedList<BlogPost> Execute()
        {
              return new PagedDataCache<BlogPost>().Only(post => post.Author.Slug == Context.Item1)
                                                   .OnPage(Context.Item2)
                                                   .Sorted(p => p.PostDate, false)
                                                   .With(x => x.With(p => p.Author))
                                                   .CachedValue;
        }

        #endregion
    }
}