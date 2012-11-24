﻿#region license

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
using ClubStarterKit.Infrastructure;
using ClubStarterKit.Infrastructure.Cache;
using ClubStarterKit.Web.ViewData.Membership;
using StructureMap;

namespace ClubStarterKit.Web.Infrastructure.Membership.Actions
{
    public class UserRegistractionAction : IDataAction<RegistrationViewData, bool>
    {
        public UserRegistractionAction(RegistrationViewData reg)
        {
            Context = reg;
            PasswordProvider = ObjectFactory.GetInstance<IPasswordCryptographyProvider>();
        }

        public RegistrationViewData Context { get; private set; }
        public IPasswordCryptographyProvider PasswordProvider { get; set; }

        public bool Execute()
        {
            var salt = PasswordProvider.GenerateSalt();
            var user = new User
            {
                Email = Context.Email,
                FirstName = Context.FirstName,
                LastName = Context.LastName,
                Username = Context.Username,
                PasswordSalt = salt,
                SaltedPassword = PasswordProvider.GeneratePassword(Context.Password, salt)
            };

            try
            {

                using (var scope = new UnitOfWorkScope())
                {
                    scope.UnitOfWork.RepositoryFor<User>().Save(user);
                    scope.Commit();
                }

                CacheKeyStore.ExpireWithType<User>();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}