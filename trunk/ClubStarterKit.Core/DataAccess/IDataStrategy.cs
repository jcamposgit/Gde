﻿#region license

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

namespace ClubStarterKit.Core.DataAccess
{
    /// <summary>
    /// Strategy for transforming a repository every time it is used
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IDataStrategy<TEntity>
        where TEntity : IDataModel
    {
        /// <summary>
        /// Transform a repository and return the same repository
        /// </summary>
        /// <param name="repository"><see cref="IRepository{TEntity}"/> to transform</param>
        /// <returns>A <see cref="IRepository{TEntity}"/> to be used for data access</returns>
        IRepository<TEntity> Transform(IRepository<TEntity> repository);
    }
}