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
using ClubStarterKit.Infrastructure.Cache;

namespace ClubStarterKit.Web.Infrastructure.Photo
{
    public class PhotoDeleteAction : IDataAction<Tuple<int,string>, bool>
    {
        public PhotoDeleteAction(int photo, int album)
        {
            Context = new Tuple<int,string>(photo, album.ToString());
        }

        #region Implementation of IDataAction<int,bool>

        public Tuple<int, string> Context { get; private set; }
        public bool Execute()
        {
            try
            {
                using (var scope = new UnitOfWorkScope())
                {
                    var repo = scope.UnitOfWork.RepositoryFor<Domain.Photo>();
                    repo.Delete(repo.First(x => x.Id == Context.Item1));
                    scope.Commit();
                }

                CacheKeyStore.ExpireWithType<Domain.Photo>(Context.Item2);
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