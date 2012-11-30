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

namespace ClubStarterKit.Core.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Is the unit of work in a transactional state
        /// </summary>
        bool InTransaction { get; }

        /// <summary>
        /// Begins a new transaction
        /// </summary>
        /// <exception cref="InvalidOperationException">Already in transaction</exception>
        /// <returns>Created <see cref="ITransaction"/></returns>
        ITransaction BeginTransaction();
        
        /// <summary>
        /// Begins a new transaction
        /// </summary>
        /// <param name="isolationLevel">Level of transactional isolation</param>
        /// <exception cref="InvalidOperationException">Already in transaction</exception>
        /// <returns>Created <see cref="ITransaction"/></returns>
        ITransaction BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// Flushes the unit of work to the data store
        /// </summary>
        void Flush();

        /// <summary>
        /// Flushes the transaction of the unit of work
        /// </summary>
        void TransactionalFlush();

        /// <summary>
        /// Flushes the transaction of the unit of work
        /// </summary>
        /// <param name="isolationLevel"></param>
        void TransactionalFlush(IsolationLevel isolationLevel);

        /// <summary>
        /// Gets a repository for a given type
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <returns><see cref="IRepository{T}"/> for the entity</returns>
        IRepository<T> RepositoryFor<T>() where T : IDataModel;
    }
}