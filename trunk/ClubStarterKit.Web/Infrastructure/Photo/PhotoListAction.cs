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
using ClubStarterKit.Infrastructure.Cache;

namespace ClubStarterKit.Web.Infrastructure.Photo
{
    public class PhotoListAction : IDataAction<Tuple<string, int>, IPagedList<Domain.Photo>>
    {
        public PhotoListAction(string albumSlug, int index)
        {
            Context = new Tuple<string, int>(albumSlug, index);
        }

        #region Implementation of IDataAction<KeyValuePair<Album,int>,IPagedList<Photo>>

        public Tuple<string, int> Context { get; private set; }

        public IPagedList<Domain.Photo> Execute()
        {
            return new PagedDataCache<Domain.Photo>().Only(photo => photo.Album.Slug == Context.Item1)
                                                     .WithPageSize(1)
                                                     .OnPage(Context.Item2)
                                                     .Sorted(x => x.Position)
                                                     .With(repo => repo.With(x => x.Album))
                                                     .CachedValue;
        }

        #endregion
    }
}