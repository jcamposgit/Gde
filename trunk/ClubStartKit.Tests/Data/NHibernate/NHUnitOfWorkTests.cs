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
using ClubStarterKit.Data.NHibernate;
using Moq;
using NHibernate;
using Xunit;

namespace ClubStarterKit.Tests.Data.NHibernate
{
    public class NHUnitOfWorkTests
    {
        [Fact]
        public void NHUnitOfWork_Ctor_ThrowsArgumentNullException_WhenISessionParameterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new NHUnitOfWork(null));
        }

        [Fact]
        public void NHUnitOfWork_InTransaction_ShouldReturnFalse_WhenNoTransactionExists()
        {
            ISession mockSession = new Mock<ISession>().Object;
            var unitOfWork = new NHUnitOfWork(mockSession);

            Assert.False(unitOfWork.InTransaction);
        }

        [Fact]
        public void NHUnitOfWork_BeginTransaction_ShouldStartANewTransaction_WithDefaultIsolationLevel()
        {
            var mockSession = new Mock<ISession>();
            ITransaction mockTransaction = new Mock<ITransaction>().Object;

            mockSession.Setup(x => x.BeginTransaction(IsolationLevel.ReadCommitted)).Returns(mockTransaction);

            var unitOfWork = new NHUnitOfWork(mockSession.Object);
            unitOfWork.BeginTransaction();
            Assert.True(unitOfWork.InTransaction);
            mockSession.VerifyAll();
        }

        [Fact]
        public void NHUnitOfWork_BeginTransaction_ShouldStartANewTransaction_WithSpecifiedIsolatinLevel()
        {
            var mockSession = new Mock<ISession>();
            ITransaction mockTransaction = new Mock<ITransaction>().Object;

            mockSession.Setup(x => x.BeginTransaction(IsolationLevel.Snapshot)).Returns(mockTransaction);

            var unitOfWork = new NHUnitOfWork(mockSession.Object);
            unitOfWork.BeginTransaction(IsolationLevel.Snapshot);

            Assert.True(unitOfWork.InTransaction);
            mockSession.VerifyAll();
        }

        [Fact]
        public void NHUnitOfWork_BeginTransaction_ThrowsInvalidOperationException_WhenTransactionAlreadyRunning()
        {
            var mockSession = new Mock<ISession>();
            mockSession.Setup(x => x.BeginTransaction(IsolationLevel.ReadCommitted))
                .Returns(new Mock<ITransaction>().Object);

            var unitOfWork = new NHUnitOfWork(mockSession.Object);
            unitOfWork.BeginTransaction();

            Assert.Throws<InvalidOperationException>(() => unitOfWork.BeginTransaction());
        }

        [Fact]
        public void NHUnitOfWork_Flush_CallsUnderlyingISessionFlush()
        {
            var mockSession = new Mock<ISession>();
            mockSession.Setup(x => x.Flush()).Verifiable();

            new NHUnitOfWork(mockSession.Object).Flush();

            mockSession.VerifyAll();
        }

        [Fact]
        public void NHUnitOfWork_TransactionalFlush_StartsATransaction_WithDefaultIsolationAndCommits_WhenFlushSucceeds()
        {
            var mockSession = new Mock<ISession>();
            var mockTransaction = new Mock<ITransaction>();

            mockSession.Setup(x => x.BeginTransaction(IsolationLevel.ReadCommitted)).Returns(mockTransaction.Object);
            mockSession.Setup(x => x.Flush()).Verifiable();
            mockTransaction.Setup(x => x.Commit()).Verifiable();

            new NHUnitOfWork(mockSession.Object).TransactionalFlush();

            mockSession.VerifyAll();
            mockTransaction.VerifyAll();
        }

        [Fact]
        public void
            NHUnitOfWork_TransactionalFlush_StartsATransaction_WithSpecifiedIsolationLevelAndCommits_WhenFlushSucceeds()
        {
            var mockSession = new Mock<ISession>();
            var mockTransaction = new Mock<ITransaction>();

            mockSession.Setup(x => x.BeginTransaction(IsolationLevel.ReadUncommitted)).Returns(mockTransaction.Object);
            mockSession.Setup(x => x.Flush()).Verifiable();

            mockTransaction.Setup(x => x.Commit()).Verifiable();

            new NHUnitOfWork(mockSession.Object).TransactionalFlush(IsolationLevel.ReadUncommitted);

            mockSession.VerifyAll();
            mockTransaction.VerifyAll();
        }

        [Fact]
        public void NHUnitOfWork_TransactionalFlush_RollsbackTransaction_WhenFlushThrowsException()
        {
            var mockSession = new Mock<ISession>();
            var mockTransaction = new Mock<ITransaction>();


            mockSession.Setup(x => x.BeginTransaction(IsolationLevel.ReadCommitted)).Returns(mockTransaction.Object);
            mockSession.Setup(x => x.Flush()).Throws(new Exception());

            mockTransaction.Setup(x => x.Rollback());

            var unitOfWork = new NHUnitOfWork(mockSession.Object);

            try
            {
                unitOfWork.TransactionalFlush();
                Assert.True(false);
            }
            catch
            {
                Assert.True(true);
            }


            mockSession.VerifyAll();
            mockTransaction.VerifyAll();
        }

        [Fact]
        public void NHUnitOfWork_TransactionalFlush_UsesExistingTransaction_WhenTransactionAlreadyRunning()
        {
            var mockSession = new Mock<ISession>();
            var mockTransaction = new Mock<ITransaction>();

            mockSession.Setup(x => x.BeginTransaction(IsolationLevel.ReadCommitted)).Returns(mockTransaction.Object);

            var unitOfWork = new NHUnitOfWork(mockSession.Object);
            unitOfWork.BeginTransaction();
            unitOfWork.TransactionalFlush();

            mockSession.VerifyAll();
            mockTransaction.VerifyAll();
        }

        [Fact]
        public void NHUnitOfWork_ComittingTransaction_ReleasesTransactionFromUnitOfWork()
        {
            var mockSession = new Mock<ISession>();
            mockSession.Setup(x => x.BeginTransaction(IsolationLevel.ReadCommitted))
                .Returns(new Mock<ITransaction>().Object);

            var unitOfWork = new NHUnitOfWork(mockSession.Object);
            ClubStarterKit.Core.DataAccess.ITransaction transaction = unitOfWork.BeginTransaction();

            Assert.True(unitOfWork.InTransaction);
            transaction.Commit();
            Assert.False(unitOfWork.InTransaction);
            mockSession.VerifyAll();
        }

        [Fact]
        public void NHUnitOfWork_RollbackTransaction_ReleasesTransactionFromUnitOfWork()
        {
            var mockSession = new Mock<ISession>();
            mockSession.Setup(x => x.BeginTransaction(IsolationLevel.ReadCommitted))
                .Returns(new Mock<ITransaction>().Object);

            var unitOfWork = new NHUnitOfWork(mockSession.Object);
            ClubStarterKit.Core.DataAccess.ITransaction transaction = unitOfWork.BeginTransaction();

            Assert.True(unitOfWork.InTransaction);
            transaction.Rollback();
            Assert.False(unitOfWork.InTransaction);
            mockSession.VerifyAll();
        }

        [Fact]
        public void NHUnitOfWork_Dispose_UnitOfWork_Disposed_Underlying_Transaction_And_Session()
        {
            var mockSession = new Mock<ISession>();
            var mockTransaction = new Mock<ITransaction>();
            mockSession.Setup(x => x.BeginTransaction(IsolationLevel.ReadCommitted))
                .Returns(mockTransaction.Object);
            mockTransaction.Setup(x => x.Dispose()).Verifiable();
            mockSession.Setup(x => x.Dispose()).Verifiable();

            using (var unitOfWork = new NHUnitOfWork(mockSession.Object))
            {
                unitOfWork.BeginTransaction();
            }
            mockSession.VerifyAll();
            mockTransaction.VerifyAll();
        }
    }
}