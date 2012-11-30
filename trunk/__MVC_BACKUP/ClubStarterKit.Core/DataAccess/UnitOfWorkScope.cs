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
using System.Transactions;
using ClubStarterKit.Core.DataStorage;
using ClubStarterKit.Core.Extensions;
using IsolationLevel = System.Data.IsolationLevel;

namespace ClubStarterKit.Core.DataAccess
{
    /// <summary>
    /// Disposable scope for encapulating a UnitOfWork operation
    /// </summary>
    public class UnitOfWorkScope : Disposable
    {
        private static readonly string UnitOfWorkScopeStackKey = typeof (UnitOfWorkScope).FullName + ".RunningScopeStack";

        private readonly bool _autoCompleteTransaction;
        private UnitOfWorkScopeTransaction _currentTransaction;

        public UnitOfWorkScope()
            : this(GetScopeIsolationLevel())
        {
        }

        public UnitOfWorkScope(IsolationLevel isolationLevel, UnitOfWorkScopeTransactionOptions transactionOptions = UnitOfWorkScopeTransactionOptions.UseCompatible)
        {
            _autoCompleteTransaction = transactionOptions == UnitOfWorkScopeTransactionOptions.AutoComplete;
            _currentTransaction = UnitOfWorkScopeTransaction.GetTransaction(this, isolationLevel, transactionOptions);
            RegisterScope(this);
        }

        #region Public

        public static bool IsInSession
        {
            get
            {
                return DataStore.Local.Contains(UnitOfWorkScopeStackKey) && RunningScopes.Count > 0;
            }
        }

        public static UnitOfWorkScope Current
        {
            get
            {
                if (RunningScopes.Count == 0)
                    return null;
                return RunningScopes.Peek();
            }
        }        

        /// <summary>
        /// Current running Unit Of Work implementation
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get { return _currentTransaction.UnitOfWork; }
        }

        /// <summary>
        /// Commits a unit of work and any attached transaction
        /// </summary>
        public void Commit()
        {
            ThrowExceptionIfDisposed();

            _currentTransaction.Commit(this);
            _currentTransaction = null;
        }

        #endregion

        #region private static

        private static Stack<UnitOfWorkScope> RunningScopes
        {
            get
            {
                if (!DataStore.Local.Contains(UnitOfWorkScopeStackKey))
                    DataStore.Local.Set(UnitOfWorkScopeStackKey, new Stack<UnitOfWorkScope>());
                return DataStore.Local.Get<Stack<UnitOfWorkScope>>(UnitOfWorkScopeStackKey);
            }
        }

        private static IsolationLevel GetScopeIsolationLevel()
        {
            return Transaction.Current == null
                       ? IsolationLevel.ReadCommitted
                       : Transaction.Current.IsolationLevel.MapToSystemDataIsolationLevel();
        }

        private static void RegisterScope(UnitOfWorkScope scope)
        {
            if (scope == null) throw new ArgumentNullException("scope");

            DataAccess.UnitOfWork.Current = scope.UnitOfWork;
            RunningScopes.Push(scope);
        }

        private static void UnRegisterScope(UnitOfWorkScope scope)
        {
            if (scope == null) throw new ArgumentNullException("scope");
            if (RunningScopes.Peek() != scope) throw new InvalidOperationException("Running Scope is the scope");

            RunningScopes.Pop();

            if (RunningScopes.Count > 0)
            {
                UnitOfWorkScope currentScope = RunningScopes.Peek();
                DataAccess.UnitOfWork.Current = currentScope.UnitOfWork;
            }
            else
                DataAccess.UnitOfWork.Current = null;
        }

        #endregion

        #region IDisposable

        protected override void DisposeManagedResources()
        {
            if (_currentTransaction != null && !_currentTransaction.IsDisposed)
            {
                if (_autoCompleteTransaction)
                {
                    try
                    {
                        _currentTransaction.Commit(this);
                    }
                    catch
                    {
                        _currentTransaction.Rollback(this);
                        _currentTransaction = null;
                        UnRegisterScope(this);
                        throw;
                    }
                }
                else
                    _currentTransaction.Rollback(this);
                _currentTransaction = null;
            }
            UnRegisterScope(this);
        }

        #endregion
    }
}