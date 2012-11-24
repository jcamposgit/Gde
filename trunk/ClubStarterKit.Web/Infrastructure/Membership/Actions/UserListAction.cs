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

using ClubStarterKit.Core;
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure.Cache;

namespace ClubStarterKit.Web.Infrastructure.Membership
{
    public class UserListAction : IDataAction<int, IPagedList<User>>
    {
        public UserListAction(int index)
        {
            Context = index;
        }

        #region Implementation of IDataAction<int,IPagedList<User>>

        public int Context { get; private set; }
        public IPagedList<User> Execute()
        {
            return
                new PagedDataCache<User>().OnPage(Context)
                                          .Sorted(u => u.LastName)
                                          .CachedValue;
        }

        #endregion
    }
}