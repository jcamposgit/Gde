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

using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure.Application;
using ClubStarterKit.Infrastructure.Cache;
using StructureMap;

namespace ClubStarterKit.Web.Infrastructure.Membership
{
    public class UserRoleRetrievalAction : IDataAction<string, IEnumerable<string>>
    {
        public UserRoleRetrievalAction(string username)
        {
            Context = username;
        }

        #region Implementation of IDataAction<string,IEnumerable<string>>

        public string Context { get; private set; }

        public IEnumerable<string> Execute()
        {
            return new UserRoleCache(Context).CachedValue;
        }

        #endregion
    }

    public class UserRoleCache : HttpCacheBase<IEnumerable<string>>
    {
        private string _username;
        public UserRoleCache(string username)
            : base(new HttpContextWrapper(System.Web.HttpContext.Current), ObjectFactory.GetInstance<IApplicationIdProvider>())
        {
            _username = username;
        }

        public override string ContentType
        {
            get { return "UserRoles_" + _username; }
        }

        protected override IEnumerable<string> Grab()
        {
            IList<string> roles;
            using (var scope = new UnitOfWorkScope())
            {
                roles = scope.UnitOfWork.RepositoryFor<UserInRole>().Where(x => x.User.Username == _username).Select(x => x.Role.RoleName).ToList();
            }

            return roles;
        }
    }
}