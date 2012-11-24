#region license

//Copyright 2008 Ritesh Rao 

//Licensed under the Apache License, Version 2.0 (the "License"); 
//you may not use this file except in compliance with the License. 
//You may obtain a copy of the License at 

//http://www.apache.org/licenses/LICENSE-2.0 

//Unless required by applicable law or agreed to in writing, software 
//distributed under the License is distributed on an "AS IS" BASIS, 
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//See the License for the specific language governing permissions and 
//limitations under the License. 

#endregion

using System;
using System.Linq;
using System.Transactions;
using ClubStarterKit.Core.DataAccess;
using ClubStarterKit.Data.NHibernate;
using ClubStarterKit.Domain;
using Moq;
using NHibernate;
using Xunit;
using IsolationLevel=System.Data.IsolationLevel;

namespace ClubStarterKit.Tests.Data.NHibernate
{
    public class NHRepositoryTests : IDisposable
    {
        private Message m_message;
        private Thread m_th;
        private Topic m_topic;
        private User m_user;

        public NHRepositoryTests()
        {
            new DependencyBootstrapper().Bootstrap();
            new SampleDbBootstrapper().Bootstrap();

            SetupSampleData();
        }

        #region IDisposable Members

        public void Dispose()
        {
            TearDownSampleData();
        }

        #endregion

        private void SetupSampleData()
        {
            string sampleString = "sample string";
            m_user = new User
                         {
                             Address = sampleString,
                             Bio = sampleString,
                             Email = sampleString,
                             FirstName = sampleString,
                             LastName = sampleString,
                             MessagesPerPage = 10,
                             PasswordSalt = sampleString,
                             Phone = sampleString,
                             SaltedPassword = sampleString,
                             SendNewsletter = true,
                             ShowEmail = false,
                             ShowGravatar = true,
                             Signature = sampleString,
                             Username = sampleString
                         };
            m_topic = new Topic
                          {
                              Description = "desc",
                              Locked = false,
                              Position = 1,
                              Title = "topicttl",
                              VisibleToAnonymous = true,
                              TopicGroup = "gr"
                          };
            m_th = new Thread
                       {
                           Locked = false,
                           Views = 140,
                           Topic = m_topic,
                            Title = "sample title",
                             Hidden = false
                             
                       };
            m_message = new Message
                            {
                                Body = "messagebody",
                                Hidden = false,
                                SubmissionDate = new DateTimeOffset(DateTime.Now),
                                Thread = m_th,
                                User = m_user
                            };

            using (var scope = new UnitOfWorkScope())
            {
                new NHRepository<User>().Save(m_user);
                new NHRepository<Topic>().Save(m_topic);
                new NHRepository<Thread>().Save(m_th);
                new NHRepository<Message>().Save(m_message);
                scope.Commit();
            }
        }

        private void TearDownSampleData()
        {
            using (var scope = new UnitOfWorkScope())
            {
                new NHRepository<Message>().Delete(m_message);
                new NHRepository<Thread>().Delete(m_th);
                new NHRepository<User>().Delete(m_user);                
                new NHRepository<Topic>().Delete(m_topic);                
                scope.Commit();
            }
        }


        [Fact]
        public void NHRepository_Query_WithIncompatibleUnitOfWork_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() =>
                                                         {
                                                             IUnitOfWork mockUnitOfWork = new Mock<IUnitOfWork>().Object;
                                                             UnitOfWork.Current = mockUnitOfWork;
                                                             var userRepository = new NHRepository<User>();
                                                             IQueryable<User> results = from customer in userRepository
                                                                                        select customer;
                                                             UnitOfWork.Current = null;
                                                         });
        }

        [Fact]
        public void NHRepository_Query_AllowsLazyLoad_WhileUnitOfWorkStillRunning()
        {
            using (new UnitOfWorkScope())
            {
                var messageRepository = new NHRepository<Message>();
                Message result = (from message in messageRepository
                                  select message).First();

                Assert.IsAssignableFrom<User>(result.User);
                Assert.False(string.IsNullOrEmpty(result.User.FirstName));
            }
        }

        [Fact]
        public void NHRepository_Query_AllowsProjectionUsingSelectProjection()
        {
            using (new UnitOfWorkScope())
            {
                var ordersRepository = new NHRepository<Message>();
                var result = (from msg in ordersRepository
                              select new {msg.User.FirstName, msg.User.LastName, msg.Hidden, msg.Body}).First();

                Assert.False(string.IsNullOrEmpty(result.LastName));
                Assert.False(string.IsNullOrEmpty(result.FirstName));
            }
        }

        [Fact]
        public void NHRepository_Query_ThrowsLazyInitializationExceptionException_WhenLazyLoadingAfterUnitOfWorkFinished
            ()
        {
            Thread thread;
            using (new UnitOfWorkScope())
            {
                var thRepository = new NHRepository<Thread>();
                thread = (from u in thRepository select u).FirstOrDefault();
            }
            Assert.NotNull(thread);
            Assert.Throws<LazyInitializationException>(() => thread.Topic.Description);
        }

        [Fact]
        public void NHRepository_Delete_ExpressionValuePair_DeletesThreadWithCertainSlugValue()
        {
            string title = "sample slug", slug;
            using (var scope = new UnitOfWorkScope())
            {
                var thx = new Thread
                {
                    Topic = m_topic,
                    DateCreated = DateTimeOffset.Now,
                    Locked = false,
                    Title = title,
                    Views = 1,
                    Hidden = false
                };
                slug = thx.Slug;
                new NHRepository<Thread>().Save(thx);
                scope.Commit();
            }

            using (var scope = new UnitOfWorkScope())
            {
                new NHRepository<Thread>().Delete(x => x.Slug, slug);
                scope.Commit();
            }

            int count = -1;
            using (var scope = new UnitOfWorkScope())
            {
                var c = new NHRepository<Thread>().Where(th => th.Slug == slug).Count();
                count = c;
            }

            Assert.Equal(0, count);
        }

        [Fact]
        public void NHRepository_Save_NewUserSavesUser_WhenUnitOfWorkCommitted()
        {
            string sampleString = "sample" + DateTime.Now;
            var user = new User
                           {
                               Address = sampleString,
                               Bio = sampleString,
                               Email = sampleString,
                               FirstName = sampleString,
                               LastName = sampleString,
                               MessagesPerPage = 10,
                               PasswordSalt = sampleString,
                               Phone = sampleString,
                               SaltedPassword = sampleString,
                               SendNewsletter = true,
                               ShowEmail = false,
                               ShowGravatar = true,
                               Signature = sampleString,
                               Username = sampleString
                           };

            var queryForCustomer = new Func<NHRepository<User>, User>
                (
                x => (from cust in x
                      where cust.FirstName == user.FirstName && cust.LastName == user.LastName
                      select cust).FirstOrDefault()
                );

            using (var scope = new UnitOfWorkScope())
            {
                var userRepository = new NHRepository<User>();
                User recordCheckResult = queryForCustomer(userRepository);
                Assert.Null(recordCheckResult);

                userRepository.Save(user);
                scope.Commit();
            }

            //Starting a completely new unit of work and repository to check for existance.
            using (var scope = new UnitOfWorkScope())
            {
                var userRepositor = new NHRepository<User>();
                User recordCheckResult = queryForCustomer(userRepositor);
                Assert.NotNull(recordCheckResult);
                Assert.Equal(user.FirstName, recordCheckResult.FirstName);
                Assert.Equal(user.LastName, recordCheckResult.LastName);
                scope.Commit();
            }
        }

        [Fact]
        public void NHRepository_Save_DoesNotSaveNewUser_WhenUnitOfWorkAborted()
        {
            string sampleString = "sample" + DateTime.Now;
            var user = new User
                           {
                               Address = sampleString,
                               Bio = sampleString,
                               Email = sampleString,
                               FirstName = sampleString,
                               LastName = sampleString,
                               MessagesPerPage = 10,
                               PasswordSalt = sampleString,
                               Phone = sampleString,
                               SaltedPassword = sampleString,
                               SendNewsletter = true,
                               ShowEmail = false,
                               ShowGravatar = true,
                               Signature = sampleString,
                               Username = sampleString
                           };

            using (new UnitOfWorkScope())
            {
                var userRepository = new NHRepository<User>();
                User recordCheckResult = (from u in userRepository
                                          where u.Id == user.Id
                                          select u).FirstOrDefault();
                Assert.Null(recordCheckResult);

                userRepository.Save(user);
                //DO NOT CALL COMMIT TO SIMMULATE A ROLLBACK.
            }

            //Starting a completely new unit of work and repository to check for existance.
            using (var scope = new UnitOfWorkScope())
            {
                var userRepository = new NHRepository<User>();
                User recordCheckResult = (from u in userRepository
                                          where u.Id == user.Id
                                          select u).FirstOrDefault();
                Assert.Null(recordCheckResult);
                scope.Commit();
            }
        }

        [Fact]
        public void NHRepository_Save_UpdatesExistingUserRecord()
        {
            using (var scope = new UnitOfWorkScope())
            {
                var userRespotiory = new NHRepository<User>();
                User userCpy = (from o in userRespotiory
                                where o.Id == m_user.Id
                                select o).FirstOrDefault();

                Assert.NotNull(userCpy);
                userCpy.ShowGravatar = !m_user.ShowGravatar;

                scope.Commit();
            }

            using (new UnitOfWorkScope())
            {
                var userRespotiory = new NHRepository<User>();
                User userCpy = (from o in userRespotiory
                                where o.Id == m_user.Id
                                select o).FirstOrDefault();

                Assert.NotNull(userCpy);
                Assert.Equal(!m_user.ShowGravatar, userCpy.ShowGravatar);
            }
        }

        [Fact]
        public void NHRepository_Delete_DeletesUserRecord()
        {
            // add user
            string sampleString = "sample";
            var user = new User
                           {
                               Address = sampleString,
                               Bio = sampleString,
                               Email = sampleString,
                               FirstName = sampleString,
                               LastName = sampleString,
                               MessagesPerPage = 10,
                               PasswordSalt = sampleString,
                               Phone = sampleString,
                               SaltedPassword = sampleString,
                               SendNewsletter = true,
                               ShowEmail = false,
                               ShowGravatar = true,
                               Signature = sampleString,
                               Username = sampleString
                           };

            //Re-usable query to query for the matching record.
            var queryForUser = new Func<NHRepository<User>, User>
                (
                x => (from u in x
                      where u.Id == user.Id
                      select u).FirstOrDefault()
                );

            using (var scope = new UnitOfWorkScope())
            {
                var userRepository = new NHRepository<User>();
                User recordCheckResult = queryForUser(userRepository);
                Assert.Null(recordCheckResult);

                userRepository.Save(user);
                scope.Commit();
            }

            //Retrieve the record for deletion.
            using (var scope = new UnitOfWorkScope())
            {
                var userRepository = new NHRepository<User>();
                User userToDelete = queryForUser(userRepository);
                Assert.NotNull(userToDelete);
                userRepository.Delete(userToDelete);
                scope.Commit();
            }

            //Ensure customer record is deleted.
            using (new UnitOfWorkScope())
            {
                var userRespotiory = new NHRepository<User>();
                User recordCheckResult = queryForUser(userRespotiory);
                Assert.Null(recordCheckResult);
            }
        }

        //[Fact]
        //public void NHRepository_Query_Allows_Eger_Loading_Using_With()
        //{
        //    List<Order> results;
        //    using (new UnitOfWorkScope())
        //    {
        //        var ordersRepository = new NHRepository<User>();
        //        results = (from order in ordersRepository.With(x => x.)
        //                   select order).ToList();
        //    }
        //    results.ForEach(x =>
        //    {
        //        Assert.IsTrue(x.Customer is Customer);
        //        Assert.IsFalse(string.IsNullOrEmpty(x.Customer.FirstName));
        //    });
        //}

        [Fact]
        public void NHRepository_UnitOfWork_Rolledback_WhenContainingTransactionScopeRolledback()
        {
            string prevUsername;
            int id;

            using (var txScope = new TransactionScope(TransactionScopeOption.Required))
            using (var uowScope = new UnitOfWorkScope(IsolationLevel.Serializable))
            {
                var userRepository = new NHRepository<User>();
                User user = userRepository.First(x => true);

                id = user.Id;
                prevUsername = user.Username;
                user.Username = "l" + prevUsername;

                uowScope.Commit();

                //Note: txScope has not been committed
            }

            using (var uowScope = new UnitOfWorkScope())
            {
                var userRepository = new NHRepository<User>();
                User user = userRepository.Where(u => u.Id == id).SingleOrDefault();

                Assert.Equal(prevUsername, user.Username);
            }
        }

        [Fact]
        public void NHRepository_Delete_ById_DeletesRecord()
        {
            // add user
            string sampleString = "sample";
            var user = new User
            {
                Address = sampleString,
                Bio = sampleString,
                Email = sampleString,
                FirstName = sampleString,
                LastName = sampleString,
                MessagesPerPage = 10,
                PasswordSalt = sampleString,
                Phone = sampleString,
                SaltedPassword = sampleString,
                SendNewsletter = true,
                ShowEmail = false,
                ShowGravatar = true,
                Signature = sampleString,
                Username = sampleString
            };

            int id;
            // add user to repository
            using (var scope = new UnitOfWorkScope())
            {
                scope.UnitOfWork.RepositoryFor<User>().Save(user);
                scope.Commit();
                id = user.Id;
            }

            // delete
            using (var scope = new UnitOfWorkScope())
            {
                scope.UnitOfWork.RepositoryFor<User>().Delete(id);
                scope.Commit();
            }

            // verify
            int count;
            using (var scope = new UnitOfWorkScope())
            {
                count = scope.UnitOfWork.RepositoryFor<User>().Where(x => x.Id == id).Count();
                scope.Commit();
            }

            Assert.Equal(0, count);
        }

        [Fact]
        public void NHRepository_Delete_ByExpression_Deletes2Records_When2MatchCriteria()
        {
            // add user
            string sampleString = "sample";
            var user = new User
            {
                Address = sampleString,
                Bio = sampleString,
                Email = sampleString,
                FirstName = sampleString,
                LastName = sampleString,
                MessagesPerPage = 10,
                PasswordSalt = sampleString,
                Phone = sampleString,
                SaltedPassword = sampleString,
                SendNewsletter = true,
                ShowEmail = false,
                ShowGravatar = true,
                Signature = sampleString,
                Username = sampleString
            };

            var user2 = new User
            {
                Address = sampleString,
                Bio = sampleString,
                Email = sampleString,
                FirstName = sampleString,
                LastName = sampleString,
                MessagesPerPage = 10,
                PasswordSalt = sampleString,
                Phone = sampleString,
                SaltedPassword = sampleString,
                SendNewsletter = true,
                ShowEmail = false,
                ShowGravatar = true,
                Signature = sampleString,
                Username = sampleString
            };

            // add users to repository
            using (var scope = new UnitOfWorkScope())
            {
                var rep = scope.UnitOfWork.RepositoryFor<User>();
                rep.Save(user);
                rep.Save(user);
                scope.Commit();
            }

            // delete
            using (var scope = new UnitOfWorkScope())
            {
                scope.UnitOfWork.RepositoryFor<User>().Delete(x => x.Username, user.Username);
                scope.Commit();
            }

            // verify
            int count;
            using (var scope = new UnitOfWorkScope())
            {
                count = scope.UnitOfWork.RepositoryFor<User>().Where(x => x.Username == user.Username).Count();
                scope.Commit();
            }

            Assert.Equal(0, count);
        }

        [Fact]
        public void NHRepository_Update_WithOneExpr_UpdatesOneRecord()
        {
            // add user
            string sampleString = "sample";
            string updateemail_str = "someone@someone.com";
            int id;
            var user = new User
            {
                Address = sampleString,
                Bio = sampleString,
                Email = sampleString,
                FirstName = sampleString,
                LastName = sampleString,
                MessagesPerPage = 10,
                PasswordSalt = sampleString,
                Phone = sampleString,
                SaltedPassword = sampleString,
                SendNewsletter = true,
                ShowEmail = false,
                ShowGravatar = true,
                Signature = sampleString,
                Username = sampleString
            };

            using (var scope = new UnitOfWorkScope())
            {
                scope.UnitOfWork.RepositoryFor<User>().Save(user);
                scope.Commit();
                id = user.Id;
            }

            using (var scope = new UnitOfWorkScope()) 
            {
                scope.UnitOfWork.RepositoryFor<User>().Update(new ExpressionValuePair<User, string>(x => x.Email, updateemail_str),
                                                              new ExpressionValuePair<User, int>(x => x.Id, id));
                scope.Commit();
            }

            string val;

            using (var scope = new UnitOfWorkScope())
            {
                val = scope.UnitOfWork.RepositoryFor<User>().Where(x => x.Id == id).FirstOrDefault().Email;
                scope.Commit();
            }

            Assert.Equal(updateemail_str, val);


        }
    }
}