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

using System.Linq;
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure;
using ClubStarterKit.Infrastructure.Cache;
using StructureMap;

namespace ClubStarterKit.Web.Infrastructure.Membership.Actions
{
    public class ChangePasswordContext
    {
        public string Username { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordRepeat { get; set; }
        public string CurrentPassword{ get; set; } 
    }

    public class ChangePasswordAction : IDataAction<ChangePasswordContext, string>
    {
        public ChangePasswordAction(ChangePasswordContext context)
        {
            Context = context;
        }

        public ChangePasswordContext Context { get; private set; }

        public string Execute()
        {
            if (string.IsNullOrEmpty(Context.CurrentPassword))
                return "Current Password must be specified.";

            if (string.IsNullOrEmpty(Context.NewPassword))
                return "New Password must be specified.";

            if (Context.NewPassword != Context.NewPasswordRepeat)
                return "Password and password repeat do not match";

            string returnValue = "";
            using (var scope = new UnitOfWorkScope())
            {
                var repository = scope.UnitOfWork.RepositoryFor<User>();
                var cryptoProvider = ObjectFactory.GetInstance<IPasswordCryptographyProvider>();
                var user = repository.First(x => x.Username == Context.Username);

                if (!cryptoProvider.ValidatePassword(user.SaltedPassword, user.PasswordSalt, Context.CurrentPassword))
                    returnValue = "Current password is invalid.";
                else
                { 
                    user.SaltedPassword = cryptoProvider.GeneratePassword(Context.NewPassword, user.PasswordSalt);
                    repository.Save(user);
                    CacheKeyStore.ExpireWithType<User>(user.Username);
                    returnValue = "Password changed.";
                }

                scope.Commit();
            }
            return returnValue;
        }
    }
}