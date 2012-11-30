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
using System.Data;
using ClubStarterKit.Core.DataAccess;
using Moq;
using StructureMap;
using Xunit;

namespace ClubStarterKit.Tests.Core.DataAccess
{
    public class UnitOfWorkScopeTests
    {
        [Fact]
        public void UnitOfWorkScope_ScopeContructor_StartsNewUnitOfWorkSession()
        {
            var mockUOWFactory = new Mock<IUnitOfWorkFactory>();
            var mockUOW = new Mock<IUnitOfWork>();
            var mockTransaction = new Mock<ITransaction>();

            mockUOWFactory.Setup(x => x.Create()).Returns(mockUOW.Object);
            mockUOW.Setup(x => x.BeginTransaction(IsolationLevel.ReadCommitted)).Returns(mockTransaction.Object);

            ObjectFactory.Inject(mockUOWFactory.Object);

            using (var scope = new UnitOfWorkScope())
            {
                Assert.True(UnitOfWorkScope.IsInSession);
            }

            mockUOWFactory.VerifyAll();
            mockUOW.VerifyAll();
        }

        [Fact]
        public void UnitOfWorkScope_ScopeContructor_StartsNewUnitOfWorkSessionWithCorrectScope()
        {
            var mockUOWFactory = new Mock<IUnitOfWorkFactory>();
            var mockUOW = new Mock<IUnitOfWork>();
            var mockTransaction = new Mock<ITransaction>();

            mockUOWFactory.Setup(x => x.Create()).Returns(mockUOW.Object);
            mockUOW.Setup(x => x.BeginTransaction(IsolationLevel.ReadCommitted)).Returns(mockTransaction.Object);

            ObjectFactory.Inject(mockUOWFactory.Object);

            using (var scope = new UnitOfWorkScope())
            {
                Assert.Equal(UnitOfWorkScope.Current, scope);
            }

            mockUOWFactory.VerifyAll();
            mockUOW.VerifyAll();
        }

        [Fact]
        public void UnitOfWorkScope_ScopeContructor_StartsNewUnitOfWorkSessionWithCorrectUnitOfWorkInScope()
        {
            var mockUOWFactory = new Mock<IUnitOfWorkFactory>();
            var mockUOW = new Mock<IUnitOfWork>();
            var mockTransaction = new Mock<ITransaction>();

            mockUOWFactory.Setup(x => x.Create()).Returns(mockUOW.Object);
            mockUOW.Setup(x => x.BeginTransaction(IsolationLevel.ReadCommitted)).Returns(mockTransaction.Object);

            ObjectFactory.Inject(mockUOWFactory.Object);

            using (var scope = new UnitOfWorkScope())
            {
                Assert.Same(scope.UnitOfWork, mockUOW.Object);
            }

            mockUOWFactory.VerifyAll();
            mockUOW.VerifyAll();
        }

        [Fact]
        public void UnitOfWorkScope_Dispose_MakesRollbackOnTransaction()
        {
            var mockUOWFactory = new Mock<IUnitOfWorkFactory>();
            var mockUOW = new Mock<IUnitOfWork>();
            var mockTransaction = new Mock<ITransaction>();

            mockUOWFactory.Setup(x => x.Create()).Returns(mockUOW.Object);
            mockUOW.Setup(x => x.BeginTransaction(IsolationLevel.ReadCommitted)).Returns(mockTransaction.Object);
            mockUOW.Setup(x => x.Dispose());

            mockTransaction.Setup(x => x.Rollback());
            mockTransaction.Setup(x => x.Dispose());

            ObjectFactory.Inject(mockUOWFactory.Object);

            new UnitOfWorkScope().Dispose();

            Assert.False(UnitOfWorkScope.IsInSession);

            mockUOWFactory.VerifyAll();
            mockUOW.VerifyAll();
            mockTransaction.VerifyAll();
        }

        [Fact]
        public void UnitOfWorkScope_Commit_CallsCommitTransaction()
        {
            var mockUOWFactory = new Mock<IUnitOfWorkFactory>();
            var mockUOW = new Mock<IUnitOfWork>();
            var mockTransaction = new Mock<ITransaction>();

            mockUOWFactory.Setup(x => x.Create()).Returns(mockUOW.Object);
            mockUOW.Setup(x => x.BeginTransaction(IsolationLevel.ReadCommitted)).Returns(mockTransaction.Object);

            mockTransaction.Setup(x => x.Commit());

            ObjectFactory.Inject(mockUOWFactory.Object);

            using (var scope = new UnitOfWorkScope())
            {
                scope.Commit();
            }

            Assert.False(UnitOfWorkScope.IsInSession);
            mockUOWFactory.VerifyAll();
            mockUOW.VerifyAll();
            mockTransaction.VerifyAll();
        }

        [Fact]
        public void UnitOfWorkScope_Commit_CallsDisposeTransaction()
        {
            var mockUOWFactory = new Mock<IUnitOfWorkFactory>();
            var mockUOW = new Mock<IUnitOfWork>();
            var mockTransaction = new Mock<ITransaction>();

            mockUOWFactory.Setup(x => x.Create()).Returns(mockUOW.Object);
            mockUOW.Setup(x => x.BeginTransaction(IsolationLevel.ReadCommitted)).Returns(mockTransaction.Object);

            mockTransaction.Setup(x => x.Dispose());

            ObjectFactory.Inject(mockUOWFactory.Object);

            using (var scope = new UnitOfWorkScope())
            {
                scope.Commit();
            }

            Assert.False(UnitOfWorkScope.IsInSession);
            mockUOWFactory.VerifyAll();
            mockUOW.VerifyAll();
            mockTransaction.VerifyAll();
        }

        [Fact]
        public void UnitOfWorkScope_Commit_FlushUnitOfWork()
        {
            var mockUOWFactory = new Mock<IUnitOfWorkFactory>();
            var mockUOW = new Mock<IUnitOfWork>();
            var mockTransaction = new Mock<ITransaction>();

            mockUOWFactory.Setup(x => x.Create()).Returns(mockUOW.Object);
            mockUOW.Setup(x => x.BeginTransaction(IsolationLevel.ReadCommitted)).Returns(mockTransaction.Object);
            mockUOW.Setup(x => x.Flush());
            mockUOW.Setup(x => x.Dispose());

            ObjectFactory.Inject(mockUOWFactory.Object);

            using (var scope = new UnitOfWorkScope())
            {
                scope.Commit();
            }

            mockUOWFactory.VerifyAll();
            mockUOW.VerifyAll();
            mockTransaction.VerifyAll();
        }

        [Fact]
        public void UnitOfWorkScope_Commit_DisposeUnitOfWork()
        {
            var mockUOWFactory = new Mock<IUnitOfWorkFactory>();
            var mockUOW = new Mock<IUnitOfWork>();
            var mockTransaction = new Mock<ITransaction>();

            mockUOWFactory.Setup(x => x.Create()).Returns(mockUOW.Object);
            mockUOW.Setup(x => x.BeginTransaction(IsolationLevel.ReadCommitted)).Returns(mockTransaction.Object);
            mockUOW.Setup(x => x.Dispose());

            ObjectFactory.Inject(mockUOWFactory.Object);

            using (var scope = new UnitOfWorkScope())
            {
                scope.Commit();
            }

            mockUOWFactory.VerifyAll();
            mockUOW.VerifyAll();
            mockTransaction.VerifyAll();
        }

        [Fact]
        public void UnitOfWorkScope_ChildScope_UsesSameUnitOfWorkAsParentScope_WhenNoUnitOfWorkScopeTransactionOption()
        {
            var mockUOWFactory = new Mock<IUnitOfWorkFactory>();
            Mock<IUnitOfWork> mockUOW;

            mockUOWFactory.Setup(x => x.Create()).Returns(new Func<IUnitOfWork>(() =>
                                                                                    {
                                                                                        mockUOW =
                                                                                            new Mock<IUnitOfWork>();
                                                                                        var mockTrans =
                                                                                            new Mock<ITransaction>();
                                                                                        mockTrans.Setup(
                                                                                            x => x.Rollback()).Callback(
                                                                                            new Action(delegate { }));
                                                                                        mockUOW.Setup(
                                                                                            x =>
                                                                                            x.BeginTransaction(
                                                                                                IsolationLevel.
                                                                                                    Unspecified)).
                                                                                            Returns(mockTrans.Object);
                                                                                        return mockUOW.Object;
                                                                                    }));

            ObjectFactory.Inject(mockUOWFactory.Object);

            using (var parentScope = new UnitOfWorkScope())
            {
                using (var childScope = new UnitOfWorkScope())
                {
                    Assert.Same(parentScope.UnitOfWork, childScope.UnitOfWork);
                }
            }

            mockUOWFactory.VerifyAll();
        }

        [Fact]
        public void UnitOfWorkScope_ChildScope_UsesDifferentUnitOfWork_WhenDifferentIsolationLevel()
        {
            var mockUOWFactory = new Mock<IUnitOfWorkFactory>();
            Mock<IUnitOfWork> mockUOW;

            mockUOWFactory.Setup(x => x.Create())
                .Returns(new Func<IUnitOfWork>(() =>
                                                   {
                                                       mockUOW = new Mock<IUnitOfWork>();
                                                       mockUOW.Setup(
                                                           x => x.BeginTransaction(IsolationLevel.Unspecified)).Returns(
                                                           new Mock<ITransaction>().Object);
                                                       return mockUOW.Object;
                                                   }));

            ObjectFactory.Inject(mockUOWFactory.Object);
            using (var parentScope = new UnitOfWorkScope(IsolationLevel.Chaos))
            {
                using (var childScope = new UnitOfWorkScope(IsolationLevel.ReadCommitted))
                {
                    Assert.NotSame(parentScope.UnitOfWork, childScope.UnitOfWork);
                }
            }

            mockUOWFactory.VerifyAll();
        }

        [Fact]
        public void
            UnitOfWorkScope_ChildScope_UsesDifferentUnitOfWork_WhenUnitOfWorkScopeTransactionOptionIsNewTransaction()
        {
            var mockUOWFactory = new Mock<IUnitOfWorkFactory>();
            Mock<IUnitOfWork> mockUOW = null;


            mockUOWFactory.Setup(x => x.Create())
                .Returns(new Func<IUnitOfWork>(() =>
                                                   {
                                                       mockUOW = new Mock<IUnitOfWork>();
                                                       mockUOW.Setup(
                                                           x => x.BeginTransaction(IsolationLevel.ReadCommitted)).
                                                           Returns(new Mock<ITransaction>().Object);
                                                       return mockUOW.Object;
                                                   }));

            ObjectFactory.Inject(mockUOWFactory.Object);
            using (var parentScope = new UnitOfWorkScope(IsolationLevel.ReadCommitted))
            {
                using (
                    var childScope = new UnitOfWorkScope(IsolationLevel.ReadCommitted,
                                                         UnitOfWorkScopeTransactionOptions.CreateNew))
                {
                    Assert.NotSame(parentScope.UnitOfWork, childScope.UnitOfWork);
                }
            }

            mockUOWFactory.VerifyAll();
            mockUOW.VerifyAll();
        }

        [Fact]
        public void
            UnitOfWorkScope_ChildScope_UsesSameUnitOfWork_WhenSameIsolationLevelAndDefaultUnitOfWorkScopeTransactionOption
            ()
        {
            var mockUOWFactory = new Mock<IUnitOfWorkFactory>();
            var mockUOW = new Mock<IUnitOfWork>();
            var mockTransaction = new Mock<ITransaction>();

            mockUOWFactory.Setup(x => x.Create()).Returns(mockUOW.Object);
            mockUOW.Setup(x => x.BeginTransaction(IsolationLevel.ReadCommitted)).Returns(mockTransaction.Object);

            ObjectFactory.Inject(mockUOWFactory.Object);
            using (new UnitOfWorkScope(IsolationLevel.ReadCommitted))
            {
                Assert.True(UnitOfWorkScope.IsInSession);
                using (new UnitOfWorkScope(IsolationLevel.ReadCommitted))
                {
                }
            }

            mockUOWFactory.VerifyAll();
            mockUOW.VerifyAll();
            mockTransaction.VerifyAll();
        }

        [Fact]
        public void UnitOfWorkScope_ChildScopeCommit_DoesNotCommitTransaction()
        {
            var mockUOWFactory = new Mock<IUnitOfWorkFactory>();
            var mockUOW = new Mock<IUnitOfWork>();
            var mockTransaction = new Mock<ITransaction>();

            mockUOWFactory.Setup(x => x.Create()).Returns(mockUOW.Object);
            mockUOW.Setup(x => x.BeginTransaction(IsolationLevel.ReadCommitted)).Returns(mockTransaction.Object);

            ObjectFactory.Inject(mockUOWFactory.Object);
            using (new UnitOfWorkScope(IsolationLevel.ReadCommitted))
            {
                using (var childScope = new UnitOfWorkScope(IsolationLevel.ReadCommitted))
                {
                    childScope.Commit();
                }
            }

            mockUOWFactory.VerifyAll();
            mockUOW.VerifyAll();
            mockTransaction.VerifyAll();
        }

        [Fact]
        public void UnitOfWorkScope_ParentScopeCommit_AfterChildScopeCommit()
        {
            var mockUOWFactory = new Mock<IUnitOfWorkFactory>();
            var mockUOW = new Mock<IUnitOfWork>();
            var mockTransaction = new Mock<ITransaction>();

            mockUOWFactory.Setup(x => x.Create()).Returns(mockUOW.Object);
            mockUOW.Setup(x => x.BeginTransaction(IsolationLevel.ReadCommitted)).Returns(mockTransaction.Object);
            mockUOW.Setup(x => x.Flush());
            mockTransaction.Setup(x => x.Commit());


            ObjectFactory.Inject(mockUOWFactory.Object);
            using (var parentScope = new UnitOfWorkScope(IsolationLevel.ReadCommitted))
            {
                using (var childScope = new UnitOfWorkScope(IsolationLevel.ReadCommitted))
                {
                    childScope.Commit();
                }
                parentScope.Commit();
            }

            mockUOWFactory.VerifyAll();
            mockUOW.VerifyAll();
            mockTransaction.VerifyAll();
        }

        [Fact]
        public void UnitOfWorkScope_StartingUnitOfWorkWithAutoComplete_AutomaticallyCommitsWhenDisposed()
        {
            var mockUOWFactory = new Mock<IUnitOfWorkFactory>();
            var mockUOW = new Mock<IUnitOfWork>();
            var mockTransaction = new Mock<ITransaction>();

            mockUOWFactory.Setup(x => x.Create()).Returns(mockUOW.Object);
            mockUOW.Setup(x => x.BeginTransaction(IsolationLevel.ReadCommitted)).Returns(mockTransaction.Object);
            mockTransaction.Setup(x => x.Commit());

            ObjectFactory.Inject(mockUOWFactory.Object);
            new UnitOfWorkScope(IsolationLevel.ReadCommitted, UnitOfWorkScopeTransactionOptions.AutoComplete).Dispose();

            mockUOW.VerifyAll();
            mockUOWFactory.VerifyAll();
            mockTransaction.VerifyAll();
        }

        [Fact]
        public void UnitOfWorkScope_Commit_RollbackWhenCommitUnitOfWorkScopeThrowsException()
        {
            var mockUOWFactory = new Mock<IUnitOfWorkFactory>();
            var mockUOW = new Mock<IUnitOfWork>();
            var mockTransaction = new Mock<ITransaction>();

            mockUOWFactory.Setup(x => x.Create()).Returns(mockUOW.Object);
            mockUOW.Setup(x => x.BeginTransaction(IsolationLevel.ReadCommitted)).Returns(mockTransaction.Object);
            mockTransaction.Setup(x => x.Commit()).Throws<Exception>();
            mockTransaction.Setup(x => x.Rollback());

            ObjectFactory.Inject(mockUOWFactory.Object);

            try
            {
                new UnitOfWorkScope(IsolationLevel.ReadCommitted, UnitOfWorkScopeTransactionOptions.AutoComplete).
                    Dispose();
            }
            catch
            {
            }

            mockUOW.VerifyAll();
            mockUOWFactory.VerifyAll();
            mockTransaction.VerifyAll();
        }
    }
}