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
using System.Linq;
using System.Linq.Expressions;

namespace ClubStarterKit.Core.DataAccess
{
    /// <summary>
    /// Repository for accessing a data store
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IRepository<TEntity> : IQueryable<TEntity>
        where TEntity : IDataModel
    {
        /// <summary>
        /// Save an entity to the data store
        /// </summary>
        /// <param name="entity">Entity to save</param>
        void Save(TEntity entity);

        /// <summary>
        /// Deletes an entity from the data store
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes all entities where the property is equal to a given value
        /// </summary>
        /// <typeparam name="T">Property type</typeparam>
        /// <param name="prop">Property expression</param>
        /// <param name="value">Value of the property</param>
        void Delete<T>(Expression<Func<TEntity, T>> prop, T value);

        /// <summary>
        /// Deletes all entities where the <see cref="IPropertyValuePair{T}.Property"/>
        /// is equal to the <see cref="IPropertyValuePair{T}.Value"/>
        /// </summary>
        /// <typeparam name="T">Property type</typeparam>
        /// <param name="where">Restricting property</param>
        void Delete<T>(IPropertyValuePair<T> where);

        /// <summary>
        /// Deletes an entity with the given id
        /// </summary>
        /// <param name="id">ID of the entity to delete</param>
        void Delete(int id);

        /// <summary>
        /// Detaches an entity from the current session
        /// </summary>
        /// <param name="entity">Entity to detach</param>
        void Detach(TEntity entity);

        /// <summary>
        /// Attaches an entity to a session
        /// </summary>
        /// <param name="entity">Entity to attach</param>
        void Attach(TEntity entity);

        /// <summary>
        /// Refreshes an entity in the current session 
        /// </summary>
        /// <param name="entity">Entity to refresh</param>
        void Refresh(TEntity entity);

        /// <summary>
        /// Informs the repository to eager load an entity
        /// </summary>
        /// <param name="path">Property to eager load</param>
        /// <returns><see cref="IRepository{TEntity}"/> that will eager load the given property</returns>
        IRepository<TEntity> With(Expression<Func<TEntity, object>> path);

        /// <summary>
        /// Informs the repository to eager load an entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">Property to eager load</param>
        /// <returns><see cref="IRepository{TEntity}"/> that will eager load the given property</returns>
        IRepository<TEntity> With<T>(Expression<Func<T, object>> path);

        /// <summary>
        /// Performs an update to the datastore for given values
        /// </summary>
        /// <typeparam name="TSet"></typeparam>
        /// <typeparam name="TWhere"></typeparam>
        /// <param name="set">Property set values</param>
        /// <param name="where">Constraining property set</param>
        void Update<TSet, TWhere>(IPropertyValuePair<TSet> set, IPropertyValuePair<TWhere> where);

        /// <summary>
        /// Performs an update to the datastore for given values
        /// </summary>
        /// <param name="set">List of property set values</param>
        /// <param name="where">List of constraining values</param>
        void Update(IEnumerable<IPropertyValuePair<object>> set, IEnumerable<IPropertyValuePair<object>> where);
    }
}