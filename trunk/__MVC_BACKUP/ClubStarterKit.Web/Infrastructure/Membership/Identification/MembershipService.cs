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

using System.Web;
using System.Web.Security;
using ClubStarterKit.Infrastructure;
using StructureMap;

namespace ClubStarterKit.Web.Infrastructure.Membership
{
    public class MembershipService
    {
        private readonly IPasswordCryptographyProvider _passwordProvider;
        public bool SetFormsAuthentication = true;

        public MembershipService(IPasswordCryptographyProvider provider)
        {
            _passwordProvider = provider;
        }

        public MembershipService()
            : this(ObjectFactory.GetInstance<IPasswordCryptographyProvider>())
        { }

        public bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            var user = new UserRetrievalAction(username).Execute();

            var isvalid = user != null &&
                          _passwordProvider.ValidatePassword(user.SaltedPassword, user.PasswordSalt, password);

            if (isvalid && SetFormsAuthentication)
            {
                FormsAuthentication.SetAuthCookie(username, true);
                HttpContext.Current.Response.SetCookie(new HttpCookie(Constants.UserIdCookieKey, user.Username));
                HttpContext.Current.Session[Constants.UserSessionKey] = user;
            }

            return isvalid;
        }
    }
}