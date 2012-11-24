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
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ClubStarterKit.Core.DataStorage;
using StructureMap;

namespace ClubStarterKit.Core.DataAccess
{
    public class UnitOfWorkScopeTransaction : Disposable
    {
        private readonly ITransaction _innterTransaction;
        private readonly Stack<UnitOfWorkScope> _scopes = new Stack<UnitOfWorkScope>();
        private bool _transactionRolledback;

        public UnitOfWorkScopeTransaction(IUnitOfWorkFactory unitOfWorkFactory, IsolationLevel isolationLevel)
        {
            if (unitOfWorkFactory == null) 
                throw new ArgumentNullException("unitOfWorkFactory");

            UnitOfWork = unitOfWorkFactory.Create();
            _innterTransaction = UnitOfWork.BeginTransaction(isolationLevel);
            IsolationLevel = isolationLevel;
        }

        public IsolationLevel IsolationLevel { get; private set; }

        public IUnitOfWork UnitOfWork { get; private set; }

        private static IList<UnitOfWorkScopeTransaction> CurrentTransactions
        {
            get
            {
                string key = typeof (UnitOfWorkScopeTransaction).FullName;
                if (!DataStore.Local.Contains(key))
                    DataStore.Local.Set<IList<UnitOfWorkScopeTransaction>>(key, new List<UnitOfWorkScopeTransaction>());
                return DataStore.Local.Get<IList<UnitOfWorkScopeTransaction>>(key);
            }
        }

        public static UnitOfWorkScopeTransaction GetTransaction(UnitOfWorkScope scope, IsolationLevel isolationLevel,
                                                                UnitOfWorkScopeTransactionOptions options = UnitOfWorkScopeTransactionOptions.UseCompatible)
        {
            bool createNew = options.Equals(UnitOfWorkScopeTransactionOptions.CreateNew);
            bool useCompatible = options.Equals(UnitOfWorkScopeTransactionOptions.UseCompatible);

            if (useCompatible && createNew)
                throw new InvalidOperationException("Incompatable IsolationLevel and Transaction option");

            if (options == UnitOfWorkScopeTransactionOptions.UseCompatible)
            {
                UnitOfWorkScopeTransaction tr =
                    CurrentTransactions.Where(trans => trans.IsolationLevel == isolationLevel).FirstOrDefault();
                if (tr != null)
                {
                    tr.AttachScope(scope);
                    return tr;
                }
            }

            var transaction = new UnitOfWorkScopeTransaction(ObjectFactory.GetInstance<IUnitOfWorkFactory>(), isolationLevel);
            transaction.AttachScope(scope);
            CurrentTransactions.Add(transaction);
            return transaction;
        }

        private void AttachScope(UnitOfWorkScope scope)
        {
            if (scope == null) 
                throw new ArgumentNullException("scope");

            ThrowExceptionIfDisposed();
            _scopes.Push(scope);
        }

        public void Commit(UnitOfWorkScope scope)
        {
            if (_transactionRolledback) 
                throw new InvalidOperationException("Transaction has already been rolled back");
            
            if (scope == null) 
                throw new ArgumentNullException("scope");

            if (_scopes.Peek() != scope) 
                throw new InvalidOperationException("A commit on a scope must commence when the scope is the current scope in the transaction");
            
            ThrowExceptionIfDisposed();

            UnitOfWorkScope currentScope = _scopes.Pop();
            if (_scopes.Count != 0)
                return;

            try
            {
                UnitOfWork.Flush();
                _innterTransaction.Commit();
                _innterTransaction.Dispose();
                UnitOfWork.Dispose();
                CurrentTransactions.Remove(this);
            }
            catch
            {
                _scopes.Push(currentScope);
                throw;
            }
        }

        public void Rollback(UnitOfWorkScope scope)
        {
            ThrowExceptionIfDisposed();
            if (scope == null) throw new ArgumentNullException("scope");
            if (_scopes.Peek() != scope) throw new InvalidOperationException();
            _transactionRolledback = true;

            _scopes.Pop();
            if (_scopes.Count != 0)
                return;

            if (_innterTransaction != null)
            {
                _innterTransaction.Rollback();
                _innterTransaction.Dispose();
            }

            UnitOfWork.Dispose();
            CurrentTransactions.Remove(this);
        }
    }
}