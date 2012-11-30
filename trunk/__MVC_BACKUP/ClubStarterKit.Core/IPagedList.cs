#region license

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

using System.Collections.Generic;

namespace ClubStarterKit.Core
{
    public interface IPagedList<T> : IList<T>, IPagedList
    {
    }

    /// <summary>
    /// Utility list that holds only a single
    /// page of a list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPagedList
    {
        /// <summary>
        /// Total number of pages
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// Current Page (0-based)
        /// </summary>
        int PageIndex { get; }

        /// <summary>
        /// Current Page (1-based)
        /// </summary>
        int PageNumber { get; }

        /// <summary>
        /// Size of each page (1-based)
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// Is there a previous page
        /// </summary>
        bool HasPreviousPage { get; }

        /// <summary>
        /// Is there a next page
        /// </summary>
        bool HasNextPage { get; }

        /// <summary>
        /// Is on first page
        /// </summary>
        bool IsFirstPage { get; }

        /// <summary>
        /// Is on last page
        /// </summary>
        bool IsLastPage { get; }

        /// <summary>
        /// Total number of pages
        /// </summary>
        int TotalPages { get; }
    }
}