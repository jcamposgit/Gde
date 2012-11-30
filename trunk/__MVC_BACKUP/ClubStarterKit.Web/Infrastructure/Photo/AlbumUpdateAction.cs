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
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure.Cache;

namespace ClubStarterKit.Web.Infrastructure.Photo
{
    public class AlbumUpdateAction : IDataAction<Album, bool>
    {
        public AlbumUpdateAction(Album album)
        {
            Context = album;
        }

        #region Implementation of IDataAction<Album,bool>

        public Album Context { get; private set; }

        public bool Execute()
        {
            try
            {
                using (var scope = new UnitOfWorkScope())
                {
                    scope.UnitOfWork.RepositoryFor<Album>().Save(Context);
                    scope.Commit();
                }

                CacheKeyStore.ExpireWithType<Album>();
                CacheKeyStore.ExpireWithType<Domain.Photo>(Context.Slug);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}