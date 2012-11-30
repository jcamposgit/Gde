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
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Infrastructure;
using StructureMap;

namespace ClubStarterKit.Web.Infrastructure.Membership.Actions
{
    public class ForgotAction : NotifiableDataAction<string, bool>
    {
        public ForgotAction(string email)
        {
            Context = email;
        }

        protected override IEnumerable<string> Users
        {
            get { return new List<string> { Context }; }
        }

        protected override string Subject
        {
            get { return "Login Credentials for " + SiteConfig.ApplicationName; }
        }

        private string username;
        private string password;
        protected override string Body
        {
            get 
            {
                return string.Format(@"
                    Please return to the website and use the following for your login credentials.
                    Upon login, please change your password.
                    <br /><br />
                    <strong>Username</strong><br />{0}<br /><br />
                    <strong>Password</strong><br />{1}", username, password);
            }
        }

        protected override bool ExecuteAction()
        {
            try
            {
                using (var scope = new UnitOfWorkScope())
                {
                    var repository = scope.UnitOfWork.RepositoryFor<User>();
                    var provider = ObjectFactory.GetInstance<IPasswordCryptographyProvider>();

                    // find user
                    var user = repository.Where(x => x.Email == Context).FirstOrDefault();

                    // if user found, make new password and save the new password
                    if (user != null)
                    {
                        var newpwd = provider.GenerateSalt();
                        user.SaltedPassword = provider.GeneratePassword(newpwd, user.PasswordSalt);
                        
                        // save user (with new password)
                        repository.Save(user);

                        // set username and password to be sent along
                        username = user.Username;
                        password = newpwd;
                    }

                    scope.Commit();
                }

                SendMessage = !string.IsNullOrEmpty(username);
                return !string.IsNullOrEmpty(username);
            }
            catch
            {
                SendMessage = false;
                return false;
            }
        }
    }
}