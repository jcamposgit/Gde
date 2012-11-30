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
using ClubStarterKit.Core;
using ClubStarterKit.Core.DataAccess;

namespace ClubStarterKit.Data.NHibernate
{
    public class NHTransaction : Disposable, ITransaction
    {
        #region fields

        private readonly global::NHibernate.ITransaction _transaction;

        #endregion

        #region ctor

        /// <summary>
        /// Default Constructor.
        /// Creates a new instance of the <see cref="NHTransaction"/> instance.
        /// </summary>
        /// <param name="transaction">The underlying NHibernate.ITransaction instance.</param>
        public NHTransaction(global::NHibernate.ITransaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException("Expected a non null NHibernate.ITransaction instance.");
            _transaction = transaction;
        }

        #endregion

        #region Implementation of IDisposable

        protected override void DisposeManagedResources()
        {
            _transaction.Dispose();
        }

        #endregion

        #region Implementation of ITransaction

        /// <summary>
        /// Event raised when the transaction has been comitted.
        /// </summary>
        public event EventHandler TransactonComitted;

        /// <summary>
        /// Event raised when the transaction has been rolledback.
        /// </summary>
        public event EventHandler TransactionRolledback;

        /// <summary>
        /// Commits the changes made to the data store.
        /// </summary>
        public void Commit()
        {
            ThrowExceptionIfDisposed();

            _transaction.Commit();
            if (TransactonComitted != null)
                TransactonComitted(this, EventArgs.Empty);
        }

        /// <summary>
        /// Rollsback any changes made.
        /// </summary>
        public void Rollback()
        {
            ThrowExceptionIfDisposed();

            _transaction.Rollback();
            if (TransactionRolledback != null)
                TransactionRolledback(this, EventArgs.Empty);
        }

        #endregion
    }
}