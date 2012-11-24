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
using ClubStarterKit.Data.NHibernate;
using Moq;
using NHibernate;
using Xunit;

namespace ClubStarterKit.Tests.Data.NHibernate
{
    public class NHTransactionTests
    {
        [Fact]
        public void NHTransaction_Ctor_ThrowsArgumentNullException_WhenITransationParameterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new NHTransaction(null));
        }

        [Fact]
        public void NHTransaction_Commit_CallsCommitOnUnderlyingITransaction()
        {
            var mockTransaction = new Mock<ITransaction>();
            mockTransaction.Setup(x => x.Commit()).Verifiable();

            var transaction = new NHTransaction(mockTransaction.Object);
            transaction.Commit();

            mockTransaction.VerifyAll();
        }

        [Fact]
        public void NHTransaction_Rollback_CallsRollbackOnUnderlyingITransaction()
        {
            var mockTransaction = new Mock<ITransaction>();
            mockTransaction.Setup(x => x.Rollback()).Verifiable();

            var transaction = new NHTransaction(mockTransaction.Object);
            transaction.Rollback();

            mockTransaction.VerifyAll();
        }

        [Fact]
        public void NHTransaction_Commit_RaisesTransactionComittedEvent()
        {
            var mockTransaction = new Mock<ITransaction>();
            mockTransaction.Setup(x => x.Commit()).Verifiable();

            bool commitCalled = false;
            var transaction = new NHTransaction(mockTransaction.Object);
            transaction.TransactonComitted += delegate { commitCalled = true; };

            transaction.Commit();

            mockTransaction.VerifyAll();
            Assert.True(commitCalled);
        }

        [Fact]
        public void NHTransaction_Commit_DoesntRaiseTransactionRolledBackEvent()
        {
            var mockTransaction = new Mock<ITransaction>();
            mockTransaction.Setup(x => x.Commit()).Verifiable();

            bool rollbackCalled = false;
            var transaction = new NHTransaction(mockTransaction.Object);
            transaction.TransactionRolledback += delegate { rollbackCalled = true; };

            transaction.Commit();

            mockTransaction.VerifyAll();
            Assert.False(rollbackCalled);
        }

        [Fact]
        public void NHTransaction_Rollback_RaisesRollbackComittedEvent()
        {
            var mockTransaction = new Mock<ITransaction>();
            mockTransaction.Setup(x => x.Rollback()).Verifiable();

            bool commitCalled = false;
            var transaction = new NHTransaction(mockTransaction.Object);
            transaction.TransactonComitted += delegate { commitCalled = true; };

            transaction.Rollback();

            mockTransaction.VerifyAll();
            Assert.False(commitCalled);
        }

        [Fact]
        public void NHTransaction_Rollback_DoesntRaiseComittedEvent()
        {
            var mockTransaction = new Mock<ITransaction>();
            mockTransaction.Setup(x => x.Rollback()).Verifiable();

            bool rollbackCalled = false;
            var transaction = new NHTransaction(mockTransaction.Object);
            transaction.TransactionRolledback += delegate { rollbackCalled = true; };

            transaction.Rollback();

            mockTransaction.VerifyAll();
            Assert.True(rollbackCalled);
        }
    }
}