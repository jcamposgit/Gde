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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Domain;
using ClubStarterKit.Web.Infrastructure.Membership;
using Moq;
using StructureMap;
using Xunit;

namespace ClubStarterKit.Tests.Web.Infrastructure.MembershipProvider
{
    public class MembershipServiceTests : IDisposable
    {
        #region Helpers

        #region Nested type: MembershipRepository

        public class MembershipRepository : IRepository<User>
        {
            public List<User> users = new List<User>();

            #region IRepository<User> Members

            public void Save(User entity)
            {
                throw new NotImplementedException();
            }

            public void Delete(User entity)
            {
                throw new NotImplementedException();
            }

            public void Detach(User entity)
            {
                throw new NotImplementedException();
            }

            public void Attach(User entity)
            {
                throw new NotImplementedException();
            }

            public void Refresh(User entity)
            {
                throw new NotImplementedException();
            }

            public IRepository<User> With(Expression<Func<User, object>> path)
            {
                throw new NotImplementedException();
            }

            public IRepository<User> With<T>(Expression<Func<T, object>> path)
            {
                throw new NotImplementedException();
            }

            public Type ElementType
            {
                get { throw new NotImplementedException(); }
            }

            public Expression Expression
            {
                get { return users.AsQueryable().Expression; }
            }

            public IQueryProvider Provider
            {
                get { return users.AsQueryable().Provider; }
            }

            public void Add(User entity)
            {
                users.Add(entity);
            }

            public IEnumerator<User> GetEnumerator()
            {
                return users.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return users.GetEnumerator();
            }



            public void Delete<T>(Expression<Func<User, T>> prop, T value)
            {
                throw new NotImplementedException();
            }

            public void Delete<T>(IPropertyValuePair<T> where)
            {
                throw new NotImplementedException();
            }

            public void Delete(int id)
            {
                throw new NotImplementedException();
            }

            public void Update<TSet, TWhere>(IPropertyValuePair<TSet> set, IPropertyValuePair<TWhere> where)
            {
                throw new NotImplementedException();
            }

            public void Update(IEnumerable<IPropertyValuePair<object>> set, IEnumerable<IPropertyValuePair<object>> where)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Nested type: MembershipUnitOfWork

        public class MembershipUnitOfWork : IUnitOfWork
        {
            #region IUnitOfWork Members

            public ITransaction BeginTransaction()
            {
                return new Mock<ITransaction>().Object;
            }

            public ITransaction BeginTransaction(IsolationLevel isolationLevel)
            {
                return new Mock<ITransaction>().Object;
            }

            public void Flush()
            {
            }

            public void TransactionalFlush()
            {
            }

            public void TransactionalFlush(IsolationLevel isolationLevel)
            {
            }

            public bool InTransaction
            {
                get { throw new NotImplementedException(); }
            }

            public IRepository<T> RepositoryFor<T>()
                where T : IDataModel
            {
                return ObjectFactory.GetInstance<IRepository<T>>();
            }

            public void Dispose()
            {
            }

            #endregion
        }

        #endregion

        #region Nested type: RoleRepository

        public class RoleRepository : IRepository<UserInRole>
        {
            public List<UserInRole> userInRole = new List<UserInRole>();

            #region IRepository<UserInRole> Members

            public void Save(UserInRole entity)
            {
                throw new NotImplementedException();
            }

            public void Delete(UserInRole entity)
            {
                throw new NotImplementedException();
            }

            public void Detach(UserInRole entity)
            {
                throw new NotImplementedException();
            }

            public void Attach(UserInRole entity)
            {
                throw new NotImplementedException();
            }

            public void Refresh(UserInRole entity)
            {
                throw new NotImplementedException();
            }

            public IRepository<UserInRole> With(Expression<Func<UserInRole, object>> path)
            {
                throw new NotImplementedException();
            }

            public IRepository<UserInRole> With<T>(Expression<Func<T, object>> path)
            {
                throw new NotImplementedException();
            }

            public Type ElementType
            {
                get { throw new NotImplementedException(); }
            }

            public Expression Expression
            {
                get { return userInRole.AsQueryable().Expression; }
            }

            public IQueryProvider Provider
            {
                get { return userInRole.AsQueryable().Provider; }
            }

            public void Add(UserInRole entity)
            {
                userInRole.Add(entity);
            }

            public IEnumerator<UserInRole> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IRepository<UserInRole> Members


            public void Delete(int id)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IRepository<UserInRole> Members


            public void Delete<T>(Expression<Func<UserInRole, T>> prop, T value)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IRepository<UserInRole> Members


            public void Delete(IPropertyValuePair<object> where)
            {
                throw new NotImplementedException();
            }

            public void Update(IPropertyValuePair<object> set, IPropertyValuePair<object> where)
            {
                throw new NotImplementedException();
            }

            public void Update(IEnumerable<IPropertyValuePair<object>> set, IEnumerable<IPropertyValuePair<object>> where)
            {
                throw new NotImplementedException();
            }

            #endregion


            public void Delete<T>(IPropertyValuePair<T> where)
            {
                throw new NotImplementedException();
            }

            public void Update<TSet, TWhere>(IPropertyValuePair<TSet> set, IPropertyValuePair<TWhere> where)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #endregion

        public MembershipServiceTests()
        {
            Register();
        }

        private MembershipService Service
        {
            get
            {
                var s = new MembershipService(new PasswordCryptographyProvider());
                s.SetFormsAuthentication = false;
                return s;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            ObjectFactory.ResetDefaults();
        }

        #endregion

        private void Register(IRepository<User> rep = null, IRepository<UserInRole> rolerep = null)
        {
            ObjectFactory.Configure(config =>
                                        {
                                            config.ForRequestedType<IRepository<UserInRole>>().TheDefault.Is.Object(
                                                rolerep ?? new RoleRepository());
                                            config.ForRequestedType<IRepository<User>>().TheDefault.Is.Object(rep ??
                                                                                                              new MembershipRepository
                                                                                                                  ());
                                        });
        }

        #endregion

        [Fact]
        public void MembershipProvider_ValidateUser_ReturnsFalse_WhenUsernameNull()
        {
            Assert.False(Service.ValidateUser(null, null));
        }

        [Fact]
        public void MembershipProvider_ValidateUser_ReturnsFalse_WhenUsernameEmpty()
        {
            Assert.False(Service.ValidateUser(string.Empty, null));
        }

        [Fact]
        public void MembershipProvider_ValidateUser_ReturnsFalse_WhenPasswordNull()
        {
            Assert.False(Service.ValidateUser("username", null));
        }

        [Fact]
        public void MembershipProvider_ValidateUser_ReturnsFalse_WhenPasswordEmpty()
        {
            Assert.False(Service.ValidateUser("username", string.Empty));
        }

        [Fact(Skip="Refactor to support HTTPContext")]
        public void MembershipProvider_ValidateUser_ReturnsFalse_WhenUserDoesntExist()
        {
            string username = "username", password = "password";

            Register();

            Assert.False(Service.ValidateUser(username, password));
        }

        [Fact(Skip = "Refactor to support HTTPContext")]
        public void MembershipProvider_ValidateUser_ReturnsFalse_WhenPasswordIncorrect()
        {
            string username = "username", password = "password";
            var user = new User
                           {
                               Username = username,
                               SaltedPassword = password,
                               PasswordSalt = string.Empty
                           };
            var repository = new MembershipRepository();
            repository.Add(user);

            Register(repository);

            Assert.False(Service.ValidateUser(username, password));
        }
    }
}